   M S B A M L   `   `   `  ����A  =Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DesignDesign  5   	   T PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	       Design.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Design     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	     � Title$  PBL-HR��5   	   $	��650��6   $	��800��6%    � WindowStartupLocation$ CenterScreen=�61   (  "MahApps.Metro.Controls.MetroWindow   RightWindowCommands 5
      +  %MahApps.Metro.Controls.WindowCommands  5   
   .@�5      �� $��settings��6   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��5 �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��5��6'   $��Stretch=�61   o� 5   
   -   5         o�Name$
 menu��5      $
��Left=�6   $��30��68   $	��Top=�6D   $	��792��6\   .@�5      m� $��_File��6   .@�5      m� -   5         m�Name$ NewDB��5      $
��_New��6*   5      m� 5      -   5        $ OpenDB��5      $��_Open��6+   5      m� 5      -   5        $ SaveDB��5      $��_Save��6+   5       5!      m� 5"      -   5        $ Reports��5"      $��_Reports��6(   .@�5#      m� -   5        $ Quick��5#      $��_Quick��6*   5%      m� 5&      -   5        $ Custom��5&      $��_Custom��6+   .@�5'      m� -   5        $ LoadTemplate_Item��5'      $��_Load Template��6:   5)      m� 5*      -	   5        $ NewTemplate��5*      $��_New Template��64   5,      5-      m� 5.      -
   5        $ Print��5.      $��_Print��6*   50      m� 51      -   5        $ SaveAs��51      $��_Save As��6+   53      54      55      (  "System.Windows.Controls.WebBrowser  56   
   -   5          Name$ 
HTMLViewer��56      $��0��6)   $
��auto��69   $��	10,35,0,0q�6G   $
��auto��6Z   $	��Top=�6g   $
��Left=�6   &   System.Windows.Controls.DataGrid  57   
   -   5          Name$ Users��57   +   $��2��6:   $��	10,60,0,0q�6J   $
��auto��6]   $
��auto��6k   ��6   �� 6�   � 58   
   -   5         �Name$ ControlButtons��58      $��2��6'   $
��auto��67   $��25��6E   $��	10,35,0,0q�6Q   $	��Top=�6d    59      �� 5:      $��* �6#   �� 5;      $��* �6#   �� 5<      $��* �6#   .+�5>      �� -   5        	 ��Name$		 Add��5>      $��0��6"   $	��Add��62   6@   �� 5?      -   5        $	 Remove��5?      $��Remove��6%   $��2��66   $��10,0,0,0q�6F   5C      5H      5I      