   M S B A M L   `   `   `  ����  ControlDeTesisV47    1ControlDeTesisV4.ProyectosCcstFolder.DatosLlegada   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��700��5      $	��270��5      $��Datos de Llegada��5      .��5
      � $��15,15,15,10q�6   .+�5   
   �� $��	22,27,0,0q�6   $
��Left=�5      $	��Top=�5      $��En atención al oficio:��5      �� 5   
   -   5          ��Name$  TxtOficioAtn��5      $	��230��5      $��23��5      $��
146,27,0,0q�5      $
��Left=�5      $	��Top=�5      ��5      �� *	OficioAtn+ �� Mode$ TwoWay=��� 5   
   $��
420,27,0,0q�6   $
��Left=�5      $	��Top=�5      $��
de fecha: ��5      (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DtpOficioAtn��5      $��
490,27,0,0q�5      $
��Left=�5      $	��Top=�5         SelectedDate 5       �� *
FOficioAtn+$ TwoWay=��� 5"   
   $��	22,78,0,0q�6   $
��Left=�5#      $	��Top=�5$      $"��Archivo del Oficio Recibido:��5%      �� 5&   
   -   5        $  TxtPathOficioAtn��5&      $	��298��5'      $��23��5(      $��
173,78,0,0q�5)      $
��Left=�5*      $	��Top=�5+      ��5.      �� *FileOficioAtnOrigen+$ TwoWay=� �� UpdateSourceTrigger$ 	LostFocus=�51      �� 52   
   -   5         ��Name$ 	LblPlenos��52      $��420,131,0,0q�53      $
��Left=�54      $	��Top=�55      $��Pleno de Circuito��56      $y�Hidden=�57      �� 59   
   -   5         ��Name$ BtnLoadOficioRecibido��59      $��30��5:      $��
475,78,0,0q�5;      $
��Left=�5<      $	��Top=�5=      .��5?      �� $��22��6   $��19��5@      $6��0/ControlDeTesisV4;component/Resources/browse.png��5A      5B      �� 5C   
   -   5        $ BtnViewOficioRecibido��5C      $��27��5D      $��
510,78,0,0q�5E      $
��Left=�5F      $	��Top=�5G      {�.  .��5J      �� $��17��6   $��19��5K      $9��3/ControlDeTesisV4;component/Resources/preview16.png��5L      5M      �� 5N   
   -   5         ��Name$ CbxDestinatario��5N      $	��248��5O      $��109,130,0,0q�5P      $
��Left=�5Q      $	��Top=�5R       �� DisplayMemberPath$ NombreCompleto��5S      	 �� 
IsEditable	 . {�.  
 �� SelectedValuePath$
 IdFuncionario��5X      ��5V      �� 5X      �� 5Y   
   $��
22,130,0,0q�6   $
��Left=�5Z      $	��Top=�5[      $��Destinatario��5\      �� 5]   
   -	   5        $ 
BtnGuardar��5]      $��94��5^      $��27��5_      $��541,172,0,0q�5`      $
��Left=�5a      $	��Top=�5b      $��Guardar��5d      5e      5g      