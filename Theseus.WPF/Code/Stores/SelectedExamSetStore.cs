﻿using Theseus.Domain.Models.SetRelated;

namespace Theseus.WPF.Code.Stores
{
    public class SelectedExamSetStore
    {
        public ExamSet SelectedExamSet { get; set; }
        public int CurrentMazeIndex { get; set; } = 0;
    }
}