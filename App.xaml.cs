﻿using Org.W3c.Dom;

namespace ToDo;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new NoteList());
    }
}
