   M S B A M L   `   `   `  ����K  GControlDeTesisV4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ControlDeTesisV4.ConverterControlDeTesisV4.Converter  5      0    *ControlDeTesisV4.Reportes.TesisIncompletas   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   >	converter(clr-namespace:ControlDeTesisV4.Converter     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5   	   -   5        (  "Telerik.Windows.Controls.RadWindow    WindowStartupLocation$  CenterScreen=�5   @   $!��Tesis con datos incompletos��5   	   $	��400��6.   $	��900��6;    � 	Resources 5      2   ,ControlDeTesisV4.Converter.PonentesConverter   PonenteConverter3   -ControlDeTesisV4.Converter.TipoTesisConverter  	TipoTesis��%:   &	        &	        5   
   6&     5	   
   6'   5
      .��5      � .+�5   
   *  $Telerik.Windows.Controls.RadGridView  -   5          Name$ GListado��5      $��1��5   "   $��Stretch=�5   "   $��Stretch=�5   "   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   AutoGenerateColumns .     	RowHeight$	 150Q�5   "      ShowGroupPanel .  * 	 $Telerik.Windows.Controls.DataControl   ItemsSource 5   "   ��    Columns 5      1  +Telerik.Windows.Controls.GridViewDataColumn  5      -  'Telerik.Windows.Controls.GridViewColumn   Width?	  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 100	 6-   6
  0Telerik.Windows.Controls.GridViewBoundColumnBase	 
  Header$	 F. de Recepción��5   1   
   
IsReadOnly
 .    	IsVisible .  
  DataMemberBinding 5   1   �� *
FRecepcion+ �� StringFormat$ {0:dd/MM/yyyy}��5   1     5      $	 100	 6-   $	 Oficio Recep.��5   1   
 .  .  5   1   �� *OficioRecepcion+5   1     5       $	 120	 6-   $	 Asunto��5!   1      IsFilterable .  
 .    CellTemplate 5$      �� 5%      .R�5&      �� $��12(�6(   $
��Wrap=�5(   ,   ��5'   ,   �� *Asunto+5(   ,   5)      5+        5,      $ *	 6-   $	 Rubro��5-   1    .  
 .  50      �� 51      .R�52      �� $��12(�6(   $
��Wrap=�55   ,   ��53   ,   �� *Rubro+ �� Mode$ TwoWay=�55   ,   56      58        59      $	 100	 6-   $	 F. de Resolucion��5=   1   
 .  .  5:   1   �� *FResolucion+$ {0:dd/MM/yyyy}��$ TwoWay=�5?   1     5@      $	 150	 6-   $	 Ponente��5C   1    . 
 .  5A   1   �� *	IdPonente+ �� 	Converter# [  5E   1     5G       5H      �� 5I      .R�5J      ��  �� Orientation$ Vertical=�6)   .+�5K   "   ��  ��Name$ 
BtnInfoRec��6)   -   5         � Cursor$
 Hand��5M   -   
 J� Uid 5N   -   �� *	IdTesis+.��5O   &   �� $��32��6,   $��32��5P   0   $;��5/ControlDeTesisV4;component/Resources/mailReceive.png��5Q   0   5R   #   � 5S   "   $��15��6'   �� 5T   "   $ 
BtnInfoObs��6)   -   5        $
 Hand��5V   -    5W   -   �� *	IdTesis+.��5X   &   �� $��32��6,   $��32��5Y   0   $7��1/ControlDeTesisV4;component/Resources/mailObs.png��5Z   0   5[   #   � 5\   "   $��15��6'   �� 5]   "   $ 
BtnInfoApr��6)   -   5        $
 Hand��5_   -    5`   -   �� *	IdTesis+.��5a   &   �� $��32��6,   $��32��5b   0   $8��2/ControlDeTesisV4;component/Resources/mailSend.png��5c   0   5d   #   5e      5f      5h         RowStyle 5k      �� 5l       �� 
TargetType3  -Telerik.Windows.Controls.GridView.GridViewRow"  6   .�5m      ��  �� Property � �� �� Value 63   �� *Tatj+# [ 5n      5q      5r      5s      