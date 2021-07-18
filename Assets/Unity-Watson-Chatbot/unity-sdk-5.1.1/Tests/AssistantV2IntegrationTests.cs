﻿/**
* (C) Copyright IBM Corp. 2019, 2020.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Watson.Assistant.V2;
using IBM.Watson.Assistant.V2.Model;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;

namespace IBM.Watson.Tests
{
    public class AssistantV2IntegrationTests
    {
        private AssistantService service;
        private string versionDate = "2019-02-28";
        private string assistantId;
        private string sessionId;

        [SetUp]
        public void TestSetup()
        {
            LogSystem.InstallDefaultReactors();
        }

        [UnityTest, Order(0)]
        public IEnumerator TestMessage()
        {
            service = new AssistantService(versionDate);

            while (!service.Authenticator.CanAuthenticate())
                yield return null;

            assistantId = Environment.GetEnvironmentVariable("ASSISTANT_ASSISTANT_ID");
            string sessionId = null;

            SessionResponse createSessionResponse = null;
            Log.Debug("AssistantV2IntegrationTests", "Attempting to CreateSession...");
            service.WithHeader("X-Watson-Test", "1");
            service.CreateSession(
                callback: (DetailedResponse<SessionResponse> response, IBMError error) =>
                {
                    Log.Debug("AssistantV2IntegrationTests", "result: {0}", response.Response);
                    createSessionResponse = response.Result;
                    sessionId = createSessionResponse.SessionId;
                    Assert.IsNotNull(createSessionResponse);
                    Assert.IsNotNull(response.Result.SessionId);
                    Assert.IsNull(error);
                },
                assistantId: assistantId
            );

            while (createSessionResponse == null)
                yield return null;

            MessageResponse messageResponse = null;
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...");
            service.WithHeader("X-Watson-Test", "1");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error) =>
                {
                    Log.Debug("AssistantV2IntegrationTests", "result: {0}", response.Response);
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            var input = new MessageInput()
            {
                Text = "Are you open on Christmas?",
                Options = new MessageInputOptions()
                {
                    ReturnContext = true
                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...Are you open on Christmas?");
            service.WithHeader("X-Watson-Test", "1");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error) =>
                {
                    Log.Debug("AssistantV2IntegrationTests", "result: {0}", response.Response);
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            input = new MessageInput()
            {
                Text = "What are your hours?",
                Options = new MessageInputOptions()
                {
                    ReturnContext = true
                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...What are your hours?");
            service.WithHeader("X-Watson-Test", "1");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error) =>
                {
                    Log.Debug("AssistantV2IntegrationTests", "result: {0}", response.Response);
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;

            input = new MessageInput()
            {
                Text = "I'd like to make an appointment for 12pm.",
                Options = new MessageInputOptions()
                {
                    ReturnContext = true
                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...I'd like to make an appointment for 12pm.");
            service.WithHeader("X-Watson-Test", "1");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error) =>
                {
                    Log.Debug("AssistantV2IntegrationTests", "result: {0}", response.Response);
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            input = new MessageInput()
            {
                Text = "On Friday please.",
                Options = new MessageInputOptions()
                {
                    ReturnContext = true

                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...On Friday please.");
            service.WithHeader("X-Watson-Test", "1");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error) =>
                {
                    Log.Debug("AssistantV2IntegrationTests", "result: {0}", response.Response);
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            input = new MessageInput()
            {
                Text = "Yes.",
                Options = new MessageInputOptions()
                {
                    ReturnContext = true
                }

            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...Yes.");
            service.WithHeader("X-Watson-Test", "1");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error) =>
                {
                    Log.Debug("AssistantV2IntegrationTests", "result: {0}", response.Response);
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            input = new MessageInput()
            {
                Text = "who did Watson beat on Jeopardy?",
                Options = new MessageInputOptions()
                {
                    ReturnContext = true
                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...who did Watson beat on Jeopardy?");
            service.WithHeader("X-Watson-Test", "1");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error) =>
                {
                    Log.Debug("AssistantV2IntegrationTests", "result: {0} {1}", response.Response, assistantId);
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
            );

            while (messageResponse == null)
                yield return null;

            object deleteSessionResponse = null;
            Log.Debug("AssistantV2IntegrationTests", "Attempting to DeleteSession...");
            service.WithHeader("X-Watson-Test", "1");
            service.DeleteSession(
                callback: (DetailedResponse<object> response, IBMError error) =>
                {
                    Log.Debug("AssistantV2IntegrationTests", "result: {0}", response.Response);
                    deleteSessionResponse = response.Result;
                    Assert.IsNotNull(response.Result);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId
            );

            while (deleteSessionResponse == null)
                yield return null;
        }

        // [UnityTest, Order(1)]
        public IEnumerator TestListLogs()
        {
            service = new AssistantService(versionDate);

            while (!service.Authenticator.CanAuthenticate())
                yield return null;

            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to GetWorkspace...");
            LogCollection listLogResponse = null;
            service.ListLogs(
                callback: (DetailedResponse<LogCollection> response, IBMError error) =>
                {
                    Log.Debug("AssistantServiceV2IntegrationTests", "GetWorkspace result: {0}", response.Response);
                    listLogResponse = response.Result;
                    Assert.IsNotNull(listLogResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId
            );

            while (listLogResponse == null)
                yield return null;
        }

        [UnityTest, Order(2)]
        public IEnumerator TestRuntimeResponseGenericRuntimeResponseTypeChannelTransfer()
        {
            assistantId = Environment.GetEnvironmentVariable("ASSISTANT_ASSISTANT_ID");
            MessageResponseStateless messageResponse = null;
            string conversationId = null;

            service.WithHeader("X-Watson-Test", "1");
            MessageInputStateless input = new MessageInputStateless();

            input.Text = "test sdk";
            input.MessageType = MessageInputStateless.MessageTypeValue.TEXT;

            Log.Debug("AssistantV1IntegrationTests", "Attempting to Message...test sdk");
            service.MessageStateless(
                callback: (DetailedResponse<MessageResponseStateless> response, IBMError error) =>
                {
                    messageResponse = response.Result;
                    RuntimeResponseGenericRuntimeResponseTypeChannelTransfer 
                      runtimeResponseGenericRuntimeResponseTypeChannelTransfer =
                      (RuntimeResponseGenericRuntimeResponseTypeChannelTransfer) messageResponse.Output.Generic[0];
                    
                    ChannelTransferInfo channelTransferInfo =
                      runtimeResponseGenericRuntimeResponseTypeChannelTransfer.TransferInfo;

                    Assert.IsNotNull(channelTransferInfo);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                input: input
            );

            while (messageResponse == null)
                yield return null;
        }

        [TearDown]
        public void TestTearDown() { }
    }
}
