﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theseus.Domain.Models.MazeRelated.MazeStructure;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeCanvasViewModel : ViewModelBase
    {
        public Maze Maze { get; set; }

        public MazeCanvasViewModel()
        {
            
        }
    }
}