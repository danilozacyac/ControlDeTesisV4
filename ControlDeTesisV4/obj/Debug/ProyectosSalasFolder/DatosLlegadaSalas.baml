   M S B A M L   `   `   `  ����  ControlDeTesisV4=    7ControlDeTesisV4.ProyectosSalasFolder.DatosLlegadaSalas   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��800��5      $	��280��5      $0��*Información general del oficio de llegada��5      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5	      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5
      .��5      � $��15q�6   .+�5   
   �� -   5         ��Name$ TxtRefOficialia��5      $	��169��5      $��23��5      $��
171,10,0,0q�5      $
��Left=�5      $	��Top=�5      ��5      �� *
Referencia+ �� Mode$ TwoWay=��� 5   
   $��8,47,0,0q�6   $
��Left=�5      $	��Top=�5      $��Oficio de Recepción:��5      �� 5   
   -   5        $ TxtOficioRecp��5      $	��169��5      $��23��5      $��
171,47,0,0q�5      $
��Left=�5      $	��Top=�5      ��5       �� *OficioRecepcion+$ TwoWay=�(  "System.Windows.Controls.DatePicker  5"   
   -   5          Name$ 	DtpFRecep��5"      $��
513,10,0,0q�5#      $
��Left=�5$      $	��Top=�5%         SelectedDate 5&      �� *
FRecepcion+$ TwoWay=��� 5(   
   $��
392,10,0,0q�6   $
��Left=�5)      $	��Top=�5*      $��Fecha de Recepción:��5+      �� 5,   
   $��
10,120,0,0q�6   $
��Left=�5-      $	��Top=�5.      $��Área emisora��5/      �� 50   
   -   5         ��Name$ CbxEmisores��50      $	��170��51      $��171,120,0,0q�52      $
��Left=�53      $	��Top=�54       �� DisplayMemberPath$ Descripcion��55       �� 
IsEditable . {�.  	 �� SelectedValuePath$	 IdDato��59      ��58      �� 59      �� 5:   
   $��392,120,0,0q�6   $
��Left=�5;      $	��Top=�5<      $��Signatario:��5=      �� 5>   
   -   5        $ CbxSignatario��5>      $	��229��5?      $��513,120,0,0q�5@      $
��Left=�5A      $	��Top=�5B      $ NombreCompleto��5C       . {�.  $	 IdFuncionario��5G      ��5F      �� 5G      �� 5H   
   $��
389,47,0,0q�6   $
��Left=�5I      $	��Top=�5J      $��Archivo Oficio:��5K      �� 5L   
   -   5        $ TxtArchivoPath��5L      $	��229��5M      $��23��5N      $��
513,47,0,0q�5O      $
��Left=�5P      $	��Top=�5Q      ��5T      �� *OfRecepcionPathOrigen+$ TwoWay=�5V      �� 5W   
   $	��163��6   $��8,17,0,0q�5X      $
��Left=�5Y      $	��Top=�5Z      $%��Referencia Oficialía de Partes��5[      �� 5\   
   -   5        
 ��Name$
 BtnLoadOficioRecibido��5\      $��30��5]      $��
679,75,0,0q�5^      $
��Left=�5_      $	��Top=�5`      .��5b      �� $��22��6   $��19��5c      $6��0/ControlDeTesisV4;component/Resources/browse.png��5d      5e      �� 5f   
   -	   5        $
 BtnViewOficioRecibido��5f      $��27��5g      $��
715,75,0,0q�5h      $
��Left=�5i      $	��Top=�5j      {�.  .��5m      �� $��17��6   $��19��5n      $9��3/ControlDeTesisV4;component/Resources/preview16.png��5o      5p      �� 5q   
   -
   5        $
 
BtnGuardar��5q      $��75��5r      $��34��5s      $��679,168,0,0q�5t      $
��Left=�5u      $	��Top=�5v      $��Guardar��5x      �� 5y   
   -   5        $
 BtnSalir��5y      $��75��5z      $��34��5{      $��588,168,0,6q�5|      $
��Left=�5}      $��Salir��5      �� 5�   
   $��23��6   $��28��5�      $��
340,10,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $>��8/ControlDeTesisV4;component/Resources/questionSquare.png��5�       � ToolTip$E ?Registro que asigna Oficialia de Partes al recibir el documento��5�      �� 5�   
   $��23��6   $��28��5�      $��
658,10,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $>��8/ControlDeTesisV4;component/Resources/questionSquare.png��5�      $8 2Fecha en que Oficialía de Partes recibe el oficio��5�      5�      5�      