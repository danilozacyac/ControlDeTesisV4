   M S B A M L   `   `   `  ����  ControlDeTesisV4?    9ControlDeTesisV4.ProyectosSalasFolder.CapturObservaciones   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��843��5      $	��577��5      $��DatosProyectoCcst��5      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5	      .��5
      � .+�5   
   �� $��	31,39,0,0q�6   $
��Left=�5      $	��Top=�5      $��Fecha de Envio:��5      (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DtFEnvio��5      $��
132,31,0,0q�5      $
��Left=�5      $	��Top=�5         SelectedDate 5      �� *FEnvio+ �� Mode$ TwoWay=��� 5   
   $��	31,92,0,0q�6   $
��Left=�5      $	��Top=�5      $��Oficio de Envio:��5      �� 5   
   -   5         ��Name$ TxtOficioEnvio��5      $	��273��5      $��23��5      $��
132,85,0,0q�5      $
��Left=�5      $	��Top=�5       �� TextWrapping$ NoWrap=�5!      ��5      �� *OficioEnvio+$ TwoWay=�5!      �� 5"   
   $��
453,92,0,0q�6   $
��Left=�5#      $	��Top=�5$      $��Archivo oficio:��5%      �� 5&   
   -   5        $ TxtOficioPathOrigen��5&      $	��227��5'      $��23��5(      $��
582,85,0,0q�5)      $
��Left=�5*      $	��Top=�5+      $ NoWrap=�5/      ��5,      �� *OficioEnvioPathOrigen+$ TwoWay=�5/      �� 50   
   -   5         ��Name$ BtnLoadPath��50      $��30��51      $��744,113,0,0q�52      $
��Left=�53      $	��Top=�54      .��56      �� $��22��6   $��19��57      $6��0/ControlDeTesisV4;component/Resources/browse.png��58      59      �� 5:   
   -   5        $ 
BtnViewDoc��5:      $��30��5;      $��779,113,0,0q�5<      $
��Left=�5=      $	��Top=�5>      {�.  .��5@      �� $��22��6   $��19��5A      $9��3/ControlDeTesisV4;component/Resources/preview16.png��5B      5C      �� 5D   
   $��
29,165,0,0q�6   $
��Left=�5E      $	��Top=�5F      $��Archivo:��5G      
���  ��5H      �� 5I   
   -   5        $ TxtProyFilePath��5I      $	��273��5J      $��23��5K      $��124,165,0,0q�5L      $
��Left=�5M      $	��Top=�5N       �� AcceptsReturn .  J� 	AllowDrop . ��5Q      �� *!ComparaTesis.TObsFilePathOrigen+$ TwoWay=�5S      �� 5T   
   -   5        $ 
BtnGuardar��5T      $��75��5U      $��33��5V      $��734,483,0,0q�5W      $
��Left=�5X      $	��Top=�5Y      $��Guardar��5[      �� 5\   
   -	   5        $ BtnLoadFilePath��5\      $��30��5]      $��401,166,0,0q�5^      $
��Left=�5_      $	��Top=�5`      .��5b      �� $��22��6   $��19��5c      $6��0/ControlDeTesisV4;component/Resources/browse.png��5d      5e      �� 5f   
   -
   5        	 ��Name$	 TxtVistaPrevia��5f      $	��778��5g      $	��243��5h      $��
31,226,0,0q�5i      $
��Left=�5j      $	��Top=�5k       . 
 �� 
IsReadOnly
 .  5m      �� 5n   
   $��
31,205,0,0q�6   $
��Left=�5o      $	��Top=�5p      $��Vista previa:��5q      5s      5u      