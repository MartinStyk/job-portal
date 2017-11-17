﻿using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class QuestionDto : DtoBase
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}