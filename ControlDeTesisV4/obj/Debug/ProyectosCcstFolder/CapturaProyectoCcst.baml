   M S B A M L   `   `   `  ����K  GControlDeTesisV4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ControlDeTesisV4.ConverterControlDeTesisV4.Converter  5      >    8ControlDeTesisV4.ProyectosCcstFolder.CapturaProyectoCcst   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      >	converter(clr-namespace:ControlDeTesisV4.Converter     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $��885.052��5      $	��802��5      $	��700��5      $��CapturaProyectoCcst��5	      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5       � 	Resources 5      2   ,ControlDeTesisV4.Converter.PonentesConverter   PonenteConverter��%   &	          5   
   6&   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   $	��300��6'   �� 5      $��2* �6   �� 5      $��40 �6   .+�5   
   � $��0��6   .+�5      �� $	��845��6   $	��260��5      $��	21,25,0,0q�5      $
��Left=�5      $	��Top=�5      $��Información del Proyecto��5      .��5       � .+�5!      �� $��	22,27,0,0q�6   $
��Left=�5"      $	��Top=�5#      $��En atención al oficio:��5$      �� 5%      -   5         ��Name$ TxtOficioAtn��5%      $	��230��5&      $��23��5'      $��
146,27,0,0q�5(      $
��Left=�5)      $	��Top=�5*      ��5+      �� *	OficioAtn+ �� Mode$ TwoWay=��� 5-      $��
420,27,0,0q�6   $
��Left=�5.      $	��Top=�5/      $��
de fecha: ��50      (  "System.Windows.Controls.DatePicker  51      -   5          Name$ DtpOficioAtn��51   !   $��
490,27,0,0q�52   !   $
��Left=�53   !   $	��Top=�54   !      SelectedDate 55   !   �� *
FOficioAtn+$ TwoWay=��� 57      $��	22,78,0,0q�6   $
��Left=�58      $	��Top=�59      $"��Archivo del Oficio Recibido:��5:      �� 5;      -   5        $ TxtPathOficioAtn��5;      $	��298��5<      $��23��5=      $��
173,78,0,0q�5>      $
��Left=�5?      $	��Top=�5@      ��5C      �� *FileOficioAtnOrigen+$ TwoWay=� �� UpdateSourceTrigger$ 	LostFocus=�5F      �� 5G      $��
22,131,0,0q�6   $
��Left=�5H      $	��Top=�5I      $��
Instancia:��5J      �� 5K      -   5         ��Name$ CbxInstancia��5K      $	��248��5L      $��109,131,0,0q�5M      $
��Left=�5N      $	��Top=�5O      	 �� DisplayMemberPath$	 Descripcion��5P      
 �� SelectedValuePath$
 IdDato��5R      ��5Q      �� 5S      �� 5T      -   5         ��Name$ 	LblPlenos��5T      $��420,131,0,0q�5U      $
��Left=�5V      $	��Top=�5W      $��Pleno de Circuito��5X      $y�Hidden=�5Y      �� 5Z      -   5        $ CbxPlenosDeCircuito��5Z      $	��282��5[      $��541,131,0,0q�5\      $
��Left=�5]      $	��Top=�5^      $	 Descripcion��5_      $
 IdDato��5a      $y�Hidden=�5c      ��5`      �� 5c      �� 5d      -   5         ��Name$ BtnLoadOficioRecibido��5d      $��30��5e      $��
475,78,0,0q�5f      $
��Left=�5g      $	��Top=�5h      .��5j      �� $��22��6    $��19��5k       $6��0/ControlDeTesisV4;component/Resources/browse.png��5l       5m      �� 5n      -	   5        $ BtnViewOficioRecibido��5n      $��27��5o      $��
510,78,0,0q�5p      $
��Left=�5q      $	��Top=�5r      {�.  .��5u      �� $��17��6    $��19��5v       $9��3/ControlDeTesisV4;component/Resources/preview16.png��5w       5x      �� 5y      -
   5        $ CbxDestinatario��5y      $	��248��5z      $��109,188,0,0q�5{      $
��Left=�5|      $	��Top=�5}      $	 NombreCompleto��5~       �� 
IsEditable . $
 IdFuncionario��5�      {�.  ��5�      �� 5�   A   �� 5�      $��
22,188,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Destinatario��5�      5�      5�      5�      �� 5�   
   $��1��6   $��25,25,25,20q�5�      $��Stretch=�5�      $��Stretch=�5�      $��Proyecto de Tesis��5�      .��5�      �  5�      �� 5�      $��* �6$   �� 5�      $��25 �6$   .+�5�      �� -   5        $ BtnEliminar��5�      $��1��5�      $��75��5�      $��0,0,15,0q�5�      $��Right=�5�      $	��Top=�5�      $��Eliminar��5�      �� 5�      -   5        $ BtnAgregarProy��5�      $��1��5�      $��75��5�      $��0,0,95,7q�5�      $��Right=�5�      $��Agregar ��5�      *  $Telerik.Windows.Controls.RadGridView  5�      -   5        $��
GProyectos��5�   &   $��15,40,15,15q�5�   &   $��Stretch=�5�   &   $��Stretch=�5�   &   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   AutoGenerateColumns .     
IsReadOnly .    ShowGroupPanel .  * 	 $Telerik.Windows.Controls.DataControl   ItemsSource 5�   &   ��    Columns 5�      1  +Telerik.Windows.Controls.GridViewDataColumn  5�      -  'Telerik.Windows.Controls.GridViewColumn   Width?	  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 150	 65   6
  0Telerik.Windows.Controls.GridViewBoundColumnBase 
  Header$ F. de Resolución��5�   5      	IsVisible .  
  DataMemberBinding 5�   5   �� *Precedente.FResolucion+ �� StringFormat$ {0:dd/MM/yyyy}��5�   5     5�      $ *	 65   $ Rubro��5�   5    .  5�   5   �� *Rubro+5�   5     5�      $	 150	 65   $ Asunto��5�   5      IsFilterable .   5�   5   �� *Precedente.Asunto+5�   5     5�      $	 150	 65   $ Ponente��5�   5    .   5�   5   �� *Precedente.IdPonente+ �� 	Converter# [  5�   5   5�      5�      5�      �� 5�   
   -   5        $ 
BtnGuardar��5�      $��2��5�      $��75��5�      $��30��5�      $��0,6,25,0q�5�      $��Right=�5�      $	��Top=�5�      $��Guardar��5�      5�      5�      