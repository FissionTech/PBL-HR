   M S B A M L   `   `   `  ����G  CFBLA Members, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FBLA_MembersFBLA_Members  5   	   T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	       FBLA_Members.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FBLA_Members     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	     � Title$  PBL-HR��5   	   $	��650��6   $	��800��6%    � WindowStartupLocation$ CenterScreen=�61   (  "MahApps.Metro.Controls.MetroWindow   RightWindowCommands 5
      +  %MahApps.Metro.Controls.WindowCommands  5   
   .@�5      �� $��settings��6   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��5��6'   $��Stretch=�61   o� 5   
   -   5         o�Name$
 menu��5      $
��Left=�6   $��30��68   $	��Top=�6D   $	��792��6\   .@�5      m� $��_File��6   .@�5      m� -   5         m�Name$ NewDB��5      $
��_New��6*   5      m� 5      -   5        $ OpenDB��5      $��_Open��6+   5      m� 5      -   5        $ SaveDB��5      $��_Save��6+   5      5       m� 5!      -   5        $ Reports��5!      $��_Reports��6(   .@�5"      m� -   5        $ Quick��5"      $��_Quick��6*   .@�5#      m� -   5        $ Quick1��5#      $��_Quick Report #1��6/   5%      m� 5&      -   5        $ Quick2��5&      $��_Quick Report #2��6/   5(      5)      m� 5*      -	   5        $ Custom��5*      $��_Custom��6+   .@�5+      m� -
   5        $ LoadTemplate_Item��5+      $��_Load Template��6:   5-      m� 5.      -   5        $ NewTemplate��5.      $��_New Template��64   50      51      m� 52      -   5        $ Print��52      $��_Print��6*   54      m� 55      -   5        $ SaveAs��55      $��_Save As��6+   57      m� 58      -   5        $ 	SeniorXLS��58      $��_Senior Report to XLS��6.   5:      5;      m� 5<      -   5        $
 Help��5<      $��_Help��6%   .@�5=      m� -   5        $ Instructions��5=      $��_Instructions��61   5?      5@      5A      (  "System.Windows.Controls.WebBrowser  5B   
   -   5          Name$ 
HTMLViewer��5B      $��0��6)   $
��auto��69   $��	10,35,0,0q�6G   $
��auto��6Z   $	��Top=�6g   $
��Left=�6   &   System.Windows.Controls.DataGrid  5C   
   -   5          Name$ Users��5C   +   $��2��6:   $��	10,60,0,0q�6J   $
��auto��6]   $
��auto��6k   ��6   �� 6k   � 5D   
   -   5         �Name$ ControlButtons��5D      $��1��6'   $
��auto��67   $��25��6E   $��	0,35,15,0q�6Q   $	��Top=�6d   $��2��6|    5E      �� 5F      $��* �6#   �� 5G      $��* �6#   �� 5H      $��* �6#   .+�5J      �� -   5        	 ��Name$		 Add��5J      $��0��6"   $	��Add��62   
 J� RenderTransformOrigin$
 
1.094,0.48&�6R   $��2��6u   $��
10,0,118,0q�6�   �� 5K      -   5        $	 Remove��5K      $��Remove��6%   $��2��66   $��10,0,0,0q�6F   5O      5T      5U      