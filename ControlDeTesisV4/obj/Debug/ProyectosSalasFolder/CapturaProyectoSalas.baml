   M S B A M L   `   `   `  ����K  GControlDeTesisV4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ControlDeTesisV4.ConverterControlDeTesisV4.Converter  5      @    :ControlDeTesisV4.ProyectosSalasFolder.CapturaProyectoSalas   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      >	converter(clr-namespace:ControlDeTesisV4.Converter     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��890��5      $	��730��5      $	��700��5      $��Captura��5	      (  "Telerik.Windows.Controls.RadWindow    WindowStartupLocation$  CenterScreen=�5       � 	Resources 5      2   ,ControlDeTesisV4.Converter.PonentesConverter   PonenteConverter��%   &	          5   
   6&   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��40 �6   .+�5   
   � $��0��6   .+�5      �� $	��182��6   $��
21,25,15,0q�5      $
��Left=�5      $	��Top=�5      $��Información del Oficio��5      .��5      � .+�5!      �� -   5         ��Name$ TxtRefOficialia��5!      $	��169��5"      $��23��5#      $��
171,10,0,0q�5$      $
��Left=�5%      $	��Top=�5&      ��5'      �� *
Referencia+ �� Mode$ TwoWay=��� 5)      $��8,47,0,0q�6   $
��Left=�5*      $	��Top=�5+      $��Oficio de Recepción:��5,      �� 5-      -   5        $ TxtOficioRecp��5-      $	��169��5.      $��23��5/      $��
171,47,0,0q�50      $
��Left=�51      $	��Top=�52      ��53      �� *OficioRecepcion+$ TwoWay=�(  "System.Windows.Controls.DatePicker  55      -   5          Name$ 	DtpFRecep��55   !   $��
579,10,0,0q�56   !   $
��Left=�57   !   $	��Top=�58   !      SelectedDate 59   !   �� *
FRecepcion+$ TwoWay=��� 5;      $��
458,10,0,0q�6   $
��Left=�5<      $	��Top=�5=      $��Fecha de Recepción:��5>      �� 5?      $��
10,120,0,0q�6   $
��Left=�5@      $	��Top=�5A      $��Área emisora��5B      �� 5C      -   5         ��Name$ CbxEmisores��5C      $	��170��5D      $��171,120,0,0q�5E      $
��Left=�5F      $	��Top=�5G       �� DisplayMemberPath$ Descripcion��5H      	 �� 
IsEditable	 . {�.  
 �� SelectedValuePath$
 IdDato��5L      ��5K      �� 5L      �� 5M      $��458,120,0,0q�6   $
��Left=�5N      $	��Top=�5O      $��Signatario:��5P      �� 5Q      -   5        $ CbxSignatario��5Q      $	��229��5R      $��579,120,0,0q�5S      $
��Left=�5T      $	��Top=�5U      $ NombreCompleto��5V      	 . {�.  $
 IdFuncionario��5[      ��5Y      �� 5[      �� 5\      $��
455,47,0,0q�6   $
��Left=�5]      $	��Top=�5^      $��Archivo Oficio:��5_      �� 5`      -   5        $ TxtArchivoPath��5`      $	��229��5a      $��23��5b      $��
579,47,0,0q�5c      $
��Left=�5d      $	��Top=�5e      ��5h      �� *OfRecepcionPathOrigen+$ TwoWay=�5j      �� 5k      $	��163��6   $��8,17,0,0q�5l      $
��Left=�5m      $	��Top=�5n      $%��Referencia Oficialía de Partes��5o      �� 5p      $��8,90,0,0q�6   $
��Left=�5q      $	��Top=�5r      $��
Instancia:��5s      �� 5t      -   5        $ CbxInstancia��5t      $	��169��5u      $��
171,84,0,0q�5v      $
��Left=�5w      $	��Top=�5x      $ Descripcion��5y      $
 IdDato��5{      ��5z      �� 5|      �� 5}      -	   5         ��Name$ BtnLoadOficioRecibido��5}      $��30��5~      $��
745,75,0,0q�5      $
��Left=�5�      $	��Top=�5�      .��5�      �� $��22��6    $��19��5�       $6��0/ControlDeTesisV4;component/Resources/browse.png��5�       5�      �� 5�      -
   5        $ BtnViewOficioRecibido��5�      $��27��5�      $��
780,75,0,0q�5�      $
��Left=�5�      $	��Top=�5�      {�.  .��5�      �� $��17��6    $��19��5�       $9��3/ControlDeTesisV4;component/Resources/preview16.png��5�       5�      �� 5�      $��23��6   $��28��5�      $��	343,7,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $>��8/ControlDeTesisV4;component/Resources/questionSquare.png��5�       � ToolTip$E ?Registro que asigna Oficialia de Partes al recibir el documento��5�      �� 5�      $��23��6   $��28��5�      $��	725,9,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $>��8/ControlDeTesisV4;component/Resources/questionSquare.png��5�      $8 2Fecha en que Oficialía de Partes recibe el oficio��5�      5�      5�      5�      �� 5�   
   $��1��6   $��25,25,15,20q�5�      $��Stretch=�5�      $��Stretch=�5�      $��Proyecto de Tesis��5�      .��5�      �  5�      �� 5�      $��40 �6$   �� 5�      $��* �6$   �� 5�      $��25 �6$   .+�5�      	� -   5         	�Name$ RadProySiEje��5�      $��0��5�      $��60��5�      $��25��5�      $��138,10,617,5q�5�      $
��Left=�5�      $	�� Si��5�      	� 5�      -   5        $ RadProyNoEje��5�      $��0��5�      $��25��5�      $��178,10,574,5q�5�      $	�� No��5�      �� 5�      $��0��6   $	��105��5�      $��25��5�      $��15,10,695,5q�5�      $
��Left=�5�      $��¿Con ejecutoria (s)?��5�      *  $Telerik.Windows.Controls.RadGridView  5�      -   5        $��
GProyectos��5�   &   $��1��5�   &   $��15,40,15,15q�5�   &   $��Stretch=�5�   &   $��Stretch=�5�   &   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   AutoGenerateColumns .     
IsReadOnly .    ShowGroupPanel .  * 	 $Telerik.Windows.Controls.DataControl   ItemsSource 5�   &   ��    Columns 5�      1  +Telerik.Windows.Controls.GridViewDataColumn  5�      -  'Telerik.Windows.Controls.GridViewColumn   Width?	  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 100	 65   6
  0Telerik.Windows.Controls.GridViewBoundColumnBase 
  Header$ F. de Resolución��5�   5      	IsVisible .  
  DataMemberBinding 5�   5   �� *Precedente.FResolucion+ �� StringFormat$ {0:dd/MM/yyyy}��5�   5     5�      $ *	 65   $ Rubro��5�   5      IsFilterable .     
IsReadOnly .    CellTemplate 5�      �� 5�   "   .R�5�   &   �� $��12(�60   $
��Wrap=�5�   0   ��5�   0   �� *Rubro+5�   0   5�   #   5�        5�      $	 120	 65   $ Asunto��5�   5    .   .  5�      �� 5�   "   .R�5�   &   �� $��12(�60   $
��Wrap=�5�   0   ��5�   0   �� *Precedente.Asunto+5�   0   5�   #   5�        5�      $	 150	 65   $ Ponente��5�   5    .   5�   5   �� *Precedente.IdPonente+ �� 	Converter# [  5�   5   5�      �� 5�      $��1��6   $��	15,10,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��Total de proyectos:��5�      �� 5�      -   5        $ TxtTotalProyectos��5�      $��1��5�      $��50��5�      $��23��5�      $��	133,6,0,0q�5�      $
��Left=�5�      $	��Top=�5�      {�.  ��5      �� *Items.Count+ �� ElementName$ 
GProyectos��$ OneWay=��� 5     $��1��6   $��23��5     $��28��5     $��	183,7,0,0q�5     $
��Left=�5     $	��Top=�5     $>��8/ControlDeTesisV4;component/Resources/questionSquare.png��5	     $2 ,Número de tesis asociadas a un mismo oficio��5
     �� 5     -   5        $ BtnEliminar��5     $��2��5     $��75��5     $��0,0,15,0q�5     $��Right=�5     $	��Top=�5     $��Eliminar��5     �� 5     -   5        $ BtnAgregarProy��5     $��2��5     $��75��5     $��0,0,95,0q�5     $��Right=�5     $	��Top=�5     $��Agregar ��5     5     5     �� 5  
   -   5        $ 
BtnGuardar��5     $��2��5     $��75��5     $��30��5      $��0,6,25,0q�5!     $��Right=�5"     $	��Top=�5#     $��Guardar��5%     5)     5*     