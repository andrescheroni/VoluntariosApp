﻿using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoluntariosApp.ViewModels
{
    public class OportunidadesViewModel : BaseViewModel
    {
        public OportunidadesViewModel()
        {
            SelectedCategoryCommand = new Command<string>(OnSelectCategory);
        }

        int selectedCategoryIndex;
        public int SelectedCategoryIndex
        {
            get => selectedCategoryIndex;
            set => SetProperty(ref selectedCategoryIndex, value);
        }

        public Command<string> SelectedCategoryCommand { get; }

        void OnSelectCategory(string index)
        {
            if(int.TryParse(index,out int i))
            {
                SelectedCategoryIndex = i;
            }
        }
    }
}
