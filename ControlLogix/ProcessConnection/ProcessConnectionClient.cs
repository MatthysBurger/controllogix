using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPA_T3000.Management;
using SPPA_T3000.Operator_Station;
using System.Diagnostics;
using SPPA_T3000.Process_Connection;
using System.Windows;

namespace Process_Connection
{
    public class ProcessConnectionClient
    {
        public ProcessConnectionClient()
        {
            GetValueDictionary = new Dictionary<long, object>();

            AlarmEvents = new UnfilteredAlarmEventList();
            AlarmEvents.ClearAlarmEvents();

            m_TrendPointsData = new TrendPointDictionary();
            SimulationTime = -1.0;
        }

        static ProcessConnectionClient()
        {
            Instance = new ProcessConnectionClient();
        }

        private string IP;
        private string Port;
        private string ConnectionString;

        public static ProcessConnectionClient Instance;
        public double SimulationTime;

        public void ConnectToServer(string ip, string port, string connectionstring)
        {
            IP = ip;
            Port = port;
            ConnectionString = connectionstring;

            ProcessConnectionServer = Activator.GetObject(typeof(IProcessConnectionServer), "tcp://" + IP + ":" + Port + "/" + ConnectionString) as IProcessConnectionServer;
            AlarmConfig.LoadAlarmsConfigFile();
            AlarmEvents.ClearAlarmEvents();
        }

        public void ReconnectConnectToServer()
        {
            if (TestConnection() == false)
            {
                ProcessConnectionServer = Activator.GetObject(typeof(IProcessConnectionServer), "tcp://" + IP + ":" + Port + "/" + ConnectionString) as IProcessConnectionServer;
                AlarmEvents.ClearAlarmEvents();
            }
        }

        public bool FirstConnectionAttempted = false;
        private bool Testing = false;

        public bool TestConnection()
        {
            if (!Testing)
            {
                try
                {
                    Testing = true;
                    if (ProcessConnectionServer != null)
                    {
                        try
                        {
                            ProcessConnectionServer.CallTest();
                        }
                        catch (Exception ex)
                        {
                            ProcessConnectionServer = null;
                            Database.LogCriticalError("Could not establish connection with server : IP='" + WorkbenchWPF.ServerIP + "' Port='" + Database.ServerPort + "' - " + ex.Message);
                            return false;
                        }
                        return true;
                    }
                    return false;
                }
                finally
                {
                    Testing = false;
                    FirstConnectionAttempted = true;
                }
            }
            return false;
        }

        public int[] RemoteVersion
        {
            get
            {
                if (TestConnection() == true)
                {
                    return ProcessConnectionServer.GetVersion();
                }
                else
                {
                    return new int[] { 0, 0, 0, 0 };
                }
            }
        }

        public void GetData()
        {
            if (TestConnection() == true)
            {
                try
                {
                    if (Keys != null)
                    {
                        Object[] l_NewValues = ProcessConnectionServer.GetProperties(Keys);
                        int l_iIndexCount = 0;
                        foreach (long l_iKey in Keys)
                        {
                            Object l_NewValue = l_NewValues[l_iIndexCount];
                            GetValueDictionary[l_iKey] = l_NewValue;
                            l_iIndexCount++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Database.LogError("Exception occured during ProcessConnection GetData() : " + ex.Message);
                }
            }
        }

        public void AddGetDataPoint(int l_iAutomationFunctionId, int l_iPortId)
        {
            long l_iUniqueKey = CreateUniqueKey(l_iAutomationFunctionId, l_iPortId);
            AddGetDataPoint(l_iUniqueKey);
        }

        public void AddGetDataPoint(long l_iUniqueKey)
        {
            if (GetValueDictionary.ContainsKey(l_iUniqueKey) == false)
            {
                GetValueDictionary.Add(l_iUniqueKey, null);
                Keys = GetValueDictionary.Keys.ToArray();
            }
        }

        long[] Keys;

        public Object GetDataPoint(int l_iAutomationFunctionId, int l_iPortId)
        {
            long l_iUniqueKey = CreateUniqueKey(l_iAutomationFunctionId, l_iPortId);
            return GetDataPoint(l_iUniqueKey);
        }

        public Object GetDataPoint(long l_iUniqueKey)
        {
            Object l_Value = null;
            if (GetValueDictionary.TryGetValue(l_iUniqueKey, out l_Value) == true)
            {
                return l_Value;
            }
            return null;
        }

        public long CreateUniqueKey(int l_iAutomationFunctionId, int l_iPortId)
        {
            return l_iAutomationFunctionId + 10000000000 * l_iPortId;
        }

        public void SetDataPoint(int l_iAutomationFunctionId, int l_iPortId, Object p_Value)
        {
            if (TestConnection() == true)
            {
                long l_iUniqueKey = CreateUniqueKey(l_iAutomationFunctionId, l_iPortId);
                long[] SetValueKey = new long[] { l_iUniqueKey };
                Object p_ValueUsed = p_Value;
                Type l_ValueType = p_Value.GetType();
                switch (l_ValueType.Name.ToLower())
                {
                    case "float":
                    case "single":
                        {
                            float l_fValue = (float)p_Value;
                            double l_dValue = System.Convert.ToDouble(l_fValue);
                            p_ValueUsed = l_dValue;
                        }
                        break;
                    default:
                        break;
                }
                Object[] SetValue = new Object[] { p_ValueUsed };
                ProcessConnectionServer.SetProperties(SetValueKey, SetValue);
            }
        }

        public void FocusOnAutomationFunction(int p_iAutomationFunctionId)
        {
            if (TestConnection() == true &&
                WorkbenchWPF.Instance.EnableSimuPactSelection)
            {
                ProcessConnectionServer.FocusOnAutomationFunction(p_iAutomationFunctionId);
            }
        }

        public String GetDesignation(int p_iAutomationFunctionId)
        {
            if (TestConnection() == true)
            {
                return ProcessConnectionServer.GetDesignation(p_iAutomationFunctionId);
            }
            return String.Empty;
        }

        public int ReceiveFocusOnControllerPrototypeCommand()
        {
            if (TestConnection() == true)
            {
                return ProcessConnectionServer.ReceiveFocusOnControllerPrototypeCommand();
            }
            return 0;
        }

        public int GetDataCount
        {
            get
            {
                return GetValueDictionary.Count;
            }
        }

        public int GetTrendCount
        {
            get
            {
                return m_TrendPointsData.Count;
            }
        }

        public int GetAlarmEventCount
        {
            get
            {
                return AlarmEvents.Count;
            }
        }

        private IProcessConnectionServer ProcessConnectionServer;
        private Dictionary<long, Object> GetValueDictionary;
        public UnfilteredAlarmEventList AlarmEvents;

        public System.IO.FileInfo GetActiveAlarmSound()
        {
            Dictionary<string, System.IO.FileInfo> m_AlarmSoundFiles = new Dictionary<string, System.IO.FileInfo>();
            foreach (AlarmEvent l_AlarmEvent in AlarmEvents)
            {
                if (!l_AlarmEvent.AlarmPlayed)
                {
                    string l_sAlarmType = l_AlarmEvent.Type;
                    //if (l_AlarmEvent.Raised == "R" &&
                    //    !l_AlarmEvent.Acknowledged &&
                    //    l_AlarmEvent.Priority < l_iHighestPriority)
                    //{
                    if (!m_AlarmSoundFiles.ContainsKey(l_sAlarmType))
                    {
                        System.IO.FileInfo l_SoundFile = new System.IO.FileInfo(Database.Instance.m_RootPath.FullName + "\\Sounds\\" + Database.Instance.GetAlarmSound(l_sAlarmType));
                        if (l_SoundFile.Exists)
                        {
                            m_AlarmSoundFiles.Add(l_sAlarmType, l_SoundFile);
                        }
                    }
                    //}
                    l_AlarmEvent.AlarmPlayed = true;
                }
            }
            if (m_AlarmSoundFiles.Keys.Count > 0)
            {
                if (m_AlarmSoundFiles.ContainsKey("ALARM"))
                {
                    return m_AlarmSoundFiles["ALARM"];
                }
                else if (m_AlarmSoundFiles.ContainsKey("UNLOAD"))
                {
                    return m_AlarmSoundFiles["UNLOAD"];
                }
                else if (m_AlarmSoundFiles.ContainsKey("LDUMP"))
                {
                    return m_AlarmSoundFiles["LDUMP"];
                }
                else if (m_AlarmSoundFiles.ContainsKey("WARNING"))
                {
                    return m_AlarmSoundFiles["WARNING"];
                }
                else
                {
                    return m_AlarmSoundFiles.Values.First();
                }
            }
            else
            {
                return null;
            }
        }

        public void GetAlarmEventsData()
        {
            if (TestConnection() == true)
            {
                m_AlarmEventDatas = ProcessConnectionServer.GetAlarmEvents(AlarmEvents.CurrentSession, AlarmEvents.LatestTime);
            }
        }

        private AlarmEventData[] m_AlarmEventDatas;

        //public void UpdateAlarmEventsTest()
        //{
        //    //AlarmSequenceListControlWPF.FreezeAllUpdates();

        //    long now = DateTime.Now.Ticks;
        //    int iter = 0;
        //    foreach (long alarmID in AlarmConfig.AlarmConfigs.Keys)
        //    {
        //        AlarmEventData l_AlarmEventData = new AlarmEventData();
        //        l_AlarmEventData.ID = alarmID;
        //        l_AlarmEventData.SessionID = 1;
        //        l_AlarmEventData.TimeIn = now;
        //        l_AlarmEventData.TimeOut = now;
        //        l_AlarmEventData.LastModified = now;
        //         AlarmEvents.UpdateAlarmEvent(l_AlarmEventData);
        //        iter++;
        //        if (iter >= 100)
        //        {
        //            break;
        //        }
        //    }

        //    //AlarmSequenceListControlWPF.ResumeAllUpdates();
        //}

        public void UpdateAlarmEvents()
        {
            //Debug.WriteLine("<<<<------------(UPDATE ALARM EVENTS)--------------->>>>");
            //Debug.WriteLine("<<<<------------(       START       )--------------->>>>");
            if (ProcessConnectionServer != null && m_AlarmEventDatas != null)
            {
                for (int iter = 0; iter < m_AlarmEventDatas.Length; iter++)
                {
                    AlarmEventData l_AlarmEventData = m_AlarmEventDatas[iter];
                    if (l_AlarmEventData != null)
                    {
                        //Debug.WriteLine(l_AlarmEventData);
                        AlarmEvents.UpdateAlarmEvent(l_AlarmEventData);
                    }
                }
            }
            //Debug.WriteLine("<<<<------------(       DONE        )--------------->>>>");
        }

        public void AcknowledgeAlarm(long p_iAlarmID, long p_iTimeIn)
        {
            if (TestConnection() == true)
            {
                ProcessConnectionServer.AcknowledgeAlarm(p_iAlarmID, p_iTimeIn);
            }
        }

        public void AcknowledgeAlarms(long[] p_iAlarmIDs, long[] p_iTimeIns)
        {
            if (TestConnection() == true)
            {
                ProcessConnectionServer.AcknowledgeAlarms(p_iAlarmIDs, p_iTimeIns);
            }
        }

        public void AcknowledgeAlarms(List<int> p_AlarmSources)
        {
            if (TestConnection() == true)
            {
                List<long> l_iAlarmIDS = new List<long>();
                List<long> l_iTimeIn = new List<long>();
                for (int iter = 0; iter < AlarmEvents.Count; iter++)
                {
                    AlarmEvent l_AlarmEvent = AlarmEvents[iter];
                    if (l_AlarmEvent.Acknowledged == false && p_AlarmSources.Contains(l_AlarmEvent.AlarmSource))
                    {
                        l_iAlarmIDS.Add(l_AlarmEvent.ID);
                        l_iTimeIn.Add(l_AlarmEvent.TimeIn);
                    }
                }
                if (l_iAlarmIDS.Count > 0)
                {
                    ProcessConnectionServer.AcknowledgeAlarms(l_iAlarmIDS.ToArray(), l_iTimeIn.ToArray());
                }
            }
        }

        public void AcknowledgeAllAlarms()
        {
            if (TestConnection() == true)
            {
                ProcessConnectionServer.AcknowledgeAllAlarms();
            }
        }

        private TrendPointDictionary m_TrendPointsData;

        public void AddGetTrendPoint(int l_iAutomationFunctionId, int l_iPortId)
        {
            lock (m_TrendPointsData)
            {
                long l_iUniqueKey = CreateUniqueKey(l_iAutomationFunctionId, l_iPortId);
                if (m_TrendPointsData.ContainsKey(l_iUniqueKey) == false)
                {
                    TrendPoint l_TrendPoint = new TrendPoint();
                    l_TrendPoint.ID = l_iUniqueKey;
                    l_TrendPoint.LastestTime = 0;
                    l_TrendPoint.SessionID = -1;
                    l_TrendPoint.Data.Clear();
                    m_TrendPointsData.Add(l_iUniqueKey, l_TrendPoint);
                }
            }
        }

        public TrendPoint GetTrendPoint(int l_iAutomationFunctionId, int l_iPortId)
        {
            lock (m_TrendPointsData)
            {
                long l_iUniqueKey = CreateUniqueKey(l_iAutomationFunctionId, l_iPortId);
                TrendPoint l_TrendPoint = null;
                if (m_TrendPointsData.TryGetValue(l_iUniqueKey, out l_TrendPoint) == true)
                {
                    return l_TrendPoint;
                }
            }
            return null;
        }

        public void GetTrends()
        {
            lock (m_TrendPointsData)
            {
                if (TestConnection() == true)
                {
                    DateTime l_Now = DateTime.Now;
                    DateTime l_LastSample = l_Now.AddMinutes(-(60 * 24));
                    foreach (TrendPoint l_TrendPoint in m_TrendPointsData.Values)
                    {
                        TrendPointData[] l_TrendPointDatas = ProcessConnectionServer.GetTrendPointData(l_TrendPoint.ID, l_TrendPoint.SessionID, l_TrendPoint.LastestTime);
                        if (l_TrendPointDatas != null &&
                            l_TrendPointDatas.Length > 0)
                        {
                            foreach (TrendPointData l_TrendPointData in l_TrendPointDatas)
                            {
                                if (l_TrendPointData != null)
                                {
                                    if (l_TrendPointData.SessionID != l_TrendPoint.SessionID)
                                    {
                                        l_TrendPoint.Data.Clear();
                                        l_TrendPoint.LastestTime = 0;
                                    }
                                    l_TrendPoint.SessionID = l_TrendPointData.SessionID;
                                    if (l_TrendPointData.TimeStamp > l_TrendPoint.LastestTime)
                                    {
                                        l_TrendPoint.LastestTime = l_TrendPointData.TimeStamp;
                                    }
                                    DateTime l_TimeStamp = new DateTime(l_TrendPointData.TimeStamp);
                                    l_TrendPoint.Data.Add(new System.Windows.Point((l_TimeStamp - DateTime.MinValue).TotalSeconds, Math.Round(l_TrendPointData.Value, 4)));
                                    double l_dLastSample = (l_LastSample - DateTime.MinValue).TotalSeconds;
                                    while (l_TrendPoint.Data.Count > 0 &&
                                            l_TrendPoint.Data[0].X < l_dLastSample)
                                    {
                                        l_TrendPoint.Data.RemoveAt(0);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public float GetTrendUL(long p_iTrendPointID)
        {
            float l_fUL = 100.0f;
            if (TestConnection() == true)
            {
                return System.Convert.ToSingle(ProcessConnectionServer.GetTrendUL(p_iTrendPointID));
            }
            return l_fUL;
        }

        public float GetTrendLL(long p_iTrendPointID)
        {
            float l_fLL = 0.0f;
            if (TestConnection() == true)
            {
                return System.Convert.ToSingle(ProcessConnectionServer.GetTrendLL(p_iTrendPointID));
            }
            return l_fLL;
        }

        public String GetTrendUnit(long p_iTrendPointID)
        {
            String l_sUnit = "%";
            if (TestConnection() == true)
            {
                return ProcessConnectionServer.GetTrendUnit(p_iTrendPointID);
            }
            return l_sUnit;
        }

        public String GetTrendTag(long p_iTrendPointID)
        {
            String l_sTag = "????";
            if (TestConnection() == true)
            {
                return ProcessConnectionServer.GetTrendTag(p_iTrendPointID);
            }
            return l_sTag;
        }

        public String GetTrendDesignation(long p_iTrendPointID)
        {
            String l_sDesignation = "????";
            if (TestConnection() == true)
            {
                return ProcessConnectionServer.GetTrendDesignation(p_iTrendPointID);
            }
            return l_sDesignation;
        }

        public void ActivateTrend(long p_iTrendPointID)
        {
            if (TestConnection() == true)
            {
                ProcessConnectionServer.ActivateTrend(p_iTrendPointID);
            }
        }

        public String GetNote(int p_iAutomationFunctionId)
        {
            String l_sNote = String.Empty;
            if (TestConnection() == true)
            {
                return ProcessConnectionServer.GetNote(p_iAutomationFunctionId);
            }
            return l_sNote;
        }

        public void SetNote(int p_iAutomationFunctionId, String p_sNote)
        {
            if (TestConnection() == true)
            {
                ProcessConnectionServer.SetNote(p_sNote, p_iAutomationFunctionId);
            }
        }

        public void GetSimulationTime()
        {
            if (TestConnection() == true)
            {
                SimulationTime = ProcessConnectionServer.GetSimulationTime();
            }
            else
            {
                SimulationTime = -1.0;
            }
        }

        public void SaveSetup()
        {
            if (TestConnection() == true)
            {
                ProcessConnectionServer.SaveSetup();
            }
        }

        public int GetActiveStepForSGC(int sgc_id)
        {
            int activestep = -1;
            if (TestConnection() == true)
            {
                string sactivestep = ProcessConnectionServer.GetActiveStepForSGC(sgc_id.ToString());
                int.TryParse(sactivestep, out activestep);
            }
            return activestep;
        }

        public LogicBlock GetLogicBlock(int id)
        {
            if (TestConnection())
            {
                return ProcessConnectionServer.GetLogicBlock(id);
            }
            return null;
        }

    }
}
