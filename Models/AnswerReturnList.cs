﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class AnswerReturnList
    {
        [JsonProperty("answers")]
        public List<AnswerReturn> AnswerReturns { get; set; }
    }
}
