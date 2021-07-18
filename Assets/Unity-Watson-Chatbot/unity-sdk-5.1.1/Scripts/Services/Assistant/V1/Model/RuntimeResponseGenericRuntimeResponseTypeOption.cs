/**
* (C) Copyright IBM Corp. 2020, 2021.
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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.Assistant.V1.Model
{
    /// <summary>
    /// RuntimeResponseGenericRuntimeResponseTypeOption.
    /// </summary>
    public class RuntimeResponseGenericRuntimeResponseTypeOption : RuntimeResponseGeneric
    {
        /// <summary>
        /// The preferred type of control to display.
        /// </summary>
        public class PreferenceValue
        {
            /// <summary>
            /// Constant DROPDOWN for dropdown
            /// </summary>
            public const string DROPDOWN = "dropdown";
            /// <summary>
            /// Constant BUTTON for button
            /// </summary>
            public const string BUTTON = "button";
            
        }

        /// <summary>
        /// The type of response returned by the dialog node. The specified response type must be supported by the
        /// client application or channel.
        /// </summary>
        [JsonProperty("response_type", NullValueHandling = NullValueHandling.Ignore)]
        public new string ResponseType
        {
            get { return base.ResponseType; }
            set { base.ResponseType = value; }
        }
        /// <summary>
        /// The title or introductory text to show before the response.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public new string Title
        {
            get { return base.Title; }
            set { base.Title = value; }
        }
        /// <summary>
        /// The description to show with the the response.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public new string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
        /// <summary>
        /// An array of objects describing the options from which the user can choose.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public new List<DialogNodeOutputOptionsElement> Options
        {
            get { return base.Options; }
            set { base.Options = value; }
        }
        /// <summary>
        /// An array of objects specifying channels for which the response is intended. If **channels** is present, the
        /// response is intended for a built-in integration and should not be handled by an API client.
        /// </summary>
        [JsonProperty("channels", NullValueHandling = NullValueHandling.Ignore)]
        public new List<ResponseGenericChannel> Channels
        {
            get { return base.Channels; }
            set { base.Channels = value; }
        }
    }
}
