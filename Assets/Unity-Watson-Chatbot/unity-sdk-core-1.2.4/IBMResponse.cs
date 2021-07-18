﻿/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

namespace IBM.Cloud.SDK
{
    public class DetailedResponse<T>
    {
        /// <summary>
        /// The status code returned from the server.
        /// </summary>
        public long StatusCode { get; set; }
        /// <summary>
        /// Dictionary of headers returned by the request.
        /// </summary>
        public Dictionary<string, object> Headers { get; set; }
        /// <summary>
        /// The deserialized result.
        /// </summary>
        public T Result { get; set; }

        public string Response { get; set; }

        public DetailedResponse()
        {
            if(Headers == null)
            {
                Headers = new Dictionary<string, object>();
            }
        }
    }
}
