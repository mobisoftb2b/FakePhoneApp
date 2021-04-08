using NLog;
using RabbitMQ.Client.Events;
using RabbitMQManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakePhoneApp
{
    public class SingleManager
    {
        public delegate void Del(string value);
        RabbitMQManagersApprove m = new RabbitMQManagersApprove();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        String queueNameGlobal = "managerAppQ1";

        //string deviceUniqueID = "1234";
        public string queueName;
        Del updateTextBoxDelegate;
        string ManagerEmplId;
        string DeviceUniqueID;
        int TestNumber = 0;
        public bool isBound { get; set; }
        public SingleManager(Del callback, string managerEmplId, int testNumber=0)
        {
            isBound = false;
            updateTextBoxDelegate = callback;
            ManagerEmplId = managerEmplId;
            queueName = System.Guid.NewGuid().ToString();
            DeviceUniqueID = System.Guid.NewGuid().ToString();
            TestNumber = testNumber;
            if (m.Init($"testNumber {testNumber}"))
            {
                m.BindQueue(queueName);
                m.ReceiveMessageSubscribe(queueName, ReceiveSubscriber);
                isBound = true;
            }
        }

        private void ReceiveSubscriber(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            logger.Info($"Received! TestNumber={TestNumber} message {message}");
            updateTextBoxDelegate?.Invoke(message);
            if (message.Contains("askApprove"))
                m.SendMessageFromFakeAgent(message);
        }


        public void loginUser()
        {
            if (isBound)
            {
                m.SendMessage($@"{{""command"":""loginUser"",  ""MessageId"": ""1606209433167"", ""ManagerID"":""104"",""ManagerEmplId"":""{ManagerEmplId}"", ""Password"": ""2480"",  ""QueueName"":""{queueName}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""pushToken"":""ch_rzpWcTvqzFFxqrws2ZV:APA91bEJ3kqrXSYpAMspdOKBxA_L1ByMT75eFcHISlJHPyJuUo6_ihaaJaN0f1f1tsWosnqkdMSbyV5rkEFdnD7uzqDKy3lzEi91QqxxW1qdLgrNCYTwGxoIKgSG-W1kXQTp9zwD6G43""}}", queueNameGlobal, TestNumber);
            }
        }

        public void loginUserNew()
        {
            if (isBound)
            {
                m.SendMessage($@"{{""command"":""loginUserNew"",  ""MessageId"": ""1606209433167"", ""ManagerID"":""104"",""ManagerEmplId"":""{ManagerEmplId}"", ""Password"": ""2480"",  ""QueueName"":""{queueName}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""pushToken"":""ch_rzpWcTvqzFFxqrws2ZV:APA91bEJ3kqrXSYpAMspdOKBxA_L1ByMT75eFcHISlJHPyJuUo6_ihaaJaN0f1f1tsWosnqkdMSbyV5rkEFdnD7uzqDKy3lzEi91QqxxW1qdLgrNCYTwGxoIKgSG-W1kXQTp9zwD6G43""}}", queueNameGlobal, TestNumber);
            }
        }

        public void sendLog(string fileName, string fileContent) {
            if (isBound)
            {
                m.SendMessage($@"{{""command"":""sendLog"",  ""FileData"":""{fileContent}"", ""FileName"":""{fileName}"", ""MessageId"": ""1606209433167"", ""testNumber"": ""{TestNumber}"", ""ManagerID"":""44107"", ""ManagerEmplId"":""{ManagerEmplId}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""deviceUniqueID"":""{DeviceUniqueID}"",""QueueName"":""{queueName}""}}", queueNameGlobal, TestNumber);
            }
        }

        public void received(String agentLogId)
        {
            if (isBound)
            {
                m.SendMessage($@"{{""command"":""received"",  ""RequestStatus"":  ""1"", ""AgentMessageId"": ""{agentLogId}"", ""MessageId"": ""1606209433167"", ""testNumber"": ""{TestNumber}"", ""ManagerID"":""44107"", ""ManagerEmplId"":""{ManagerEmplId}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""deviceUniqueID"":""{DeviceUniqueID}"",""QueueName"":""{queueName}""}}", queueNameGlobal, TestNumber);
            }
        }
        public void loginUserWrong()
        {
            if (isBound)
            {
                m.SendMessage($@"{{""command"":""loginUser"",  ""MessageId"": ""1606209433167"", ""ManagerID"":""104"",""ManagerEmplId"":""Rtuu"", ""Password"": ""Rtuu"",  ""QueueName"":""{queueName}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""pushToken"":""ch_rzpWcTvqzFFxqrws2ZV:APA91bEJ3kqrXSYpAMspdOKBxA_L1ByMT75eFcHISlJHPyJuUo6_ihaaJaN0f1f1tsWosnqkdMSbyV5rkEFdnD7uzqDKy3lzEi91QqxxW1qdLgrNCYTwGxoIKgSG-W1kXQTp9zwD6G43""}}", queueNameGlobal, TestNumber);
            }
        }
        public void login()
        {
            if (isBound)
            {
                m.SendMessage($@"{{""command"":""login"", ""MessageId"": ""1606209433167"", ""testNumber"": ""{TestNumber}"", ""ManagerID"":""104"",""ManagerEmplId"":""{ManagerEmplId}"", ""QueueName"":""{queueName}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""pushToken"":""ch_rzpWcTvqzFFxqrws2ZV:APA91bEJ3kqrXSYpAMspdOKBxA_L1ByMT75eFcHISlJHPyJuUo6_ihaaJaN0f1f1tsWosnqkdMSbyV5rkEFdnD7uzqDKy3lzEi91QqxxW1qdLgrNCYTwGxoIKgSG-W1kXQTp9zwD6G43""}}", queueNameGlobal, TestNumber);
                System.Diagnostics.Debug.WriteLine($"login testNumber {TestNumber}");
            }
        }

        public void loginNew()
        {
            if (isBound)
            {
                m.SendMessage($@"{{""command"":""loginNew"", ""MessageId"": ""1606209433167"", ""testNumber"": ""{TestNumber}"", ""ManagerID"":""104"",""ManagerEmplId"":""{ManagerEmplId}"", ""QueueName"":""{queueName}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""pushToken"":""ch_rzpWcTvqzFFxqrws2ZV:APA91bEJ3kqrXSYpAMspdOKBxA_L1ByMT75eFcHISlJHPyJuUo6_ihaaJaN0f1f1tsWosnqkdMSbyV5rkEFdnD7uzqDKy3lzEi91QqxxW1qdLgrNCYTwGxoIKgSG-W1kXQTp9zwD6G43""}}", queueNameGlobal, TestNumber);
                System.Diagnostics.Debug.WriteLine($"login testNumber {TestNumber}");
            }
        }

        public void Close()
        {
            m.Close();
        }

        public void logout()
        {
            m.SendMessage($@"{{""command"":""logout"",  ""MessageId"": ""1606209433167"", ""testNumber"": ""{TestNumber}"", ""ManagerID"":""104"", ""ManagerEmplId"":""{ManagerEmplId}"", ""QueueName"":""{queueName}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""samsung"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}}}}", queueNameGlobal, TestNumber);
        }

        public void history()
        {
            m.SendMessage($@"{{""command"":""history"",  ""MessageId"": ""1606209433167"", ""testNumber"": ""{TestNumber}"", ""ManagerID"":""44107"", ""ManagerEmplId"":""{ManagerEmplId}"",""deviceInfo"":{{""{DeviceUniqueID}"":""0ce903eac38bd9fa"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""deviceUniqueID"":""{DeviceUniqueID}"",""QueueName"":""{queueName}""}}", queueNameGlobal, TestNumber);
        }

        public void refresh()
        {
            m.SendMessage($@"{{""command"":""refresh"",  ""MessageId"": ""1606209433167"", ""testNumber"": ""{TestNumber}"", ""ManagerID"":""44107"", ""ManagerEmplId"":""{ManagerEmplId}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""deviceUniqueID"":""0ce903eac38bd9fa"",""QueueName"":""{queueName}""}}", queueNameGlobal, TestNumber);
        }

        public void keepAlive()
        {
            m.SendMessage($@"{{""command"":""keepalive"", ""MessageId"": ""1606209433167"", ""ManagerID"":""104"", ""ManagerEmplId"":""{ManagerEmplId}"", ""QueueName"":""{queueName}"",""deviceInfo"":{{""DeviceUniqueID"":""{DeviceUniqueID}"",""DeviceBrand"":""google"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}}}}", queueNameGlobal, TestNumber);
        }

        public void loginSveta()
        {
            m.SendMessage($@"{{""command"":""login"",""ManagerID"":""0000"",""ManagerEmplId"":""44107"",""QueueName"":""44107-new-566cf4c9c41659f2"",""deviceInfo"":{{""DeviceUniqueID"":""566cf4c9c41659f2"",""DeviceBrand"":""samsung"",""AppVersion"":""1.2.0.14"",""DeviceName"":"""",""SystemName"":""Android""}},""pushToken"":""e9MhmdfWQSisUaY2dLiNXj:APA91bG5lcPo4XUuFaHjroaLEddKEkpbo5pgyIGvTAX5z6JPlwu0FHgbxKPWN5v2GvyaeEg20AIz_OMdjgW7s1F4goRDiG4AG6c5i98vB94I04-3273QMH8NfeUU0AUAbWN5NsnKKNu6""}}", queueNameGlobal);
        }

        public void refreshSveta()
        {
            m.SendMessage($@"{{""command"":""refresh"",""ManagerID"":""0000"",""MessageId"":1609339424105,""ManagerEmplId"":""44107"",""deviceInfo"":{{""DeviceUniqueID"":""566cf4c9c41659f2"",""DeviceBrand"":""samsung"",""AppVersion"":""1.0.10.15"",""DeviceName"":"""",""SystemName"":""Android""}},""deviceUniqueID"":""566cf4c9c41659f2"",""QueueName"":""44107-new-566cf4c9c41659f2""}}", queueNameGlobal);
        }
        //{"command":"refresh","ManagerID":"0000","MessageId":1609339424105,"ManagerEmplId":"44107","deviceInfo":{"DeviceUniqueID":"566cf4c9c41659f2","DeviceBrand":"samsung","AppVersion":"1.0.10.15","DeviceName":"","SystemName":"Android"},"deviceUniqueID":"566cf4c9c41659f2","QueueName":"44107-new-566cf4c9c41659f2”}
        public void TestNotification(string managerEmployeeId, string message) {
            m.TestNotification(managerEmployeeId, message);
        }
    }
}

