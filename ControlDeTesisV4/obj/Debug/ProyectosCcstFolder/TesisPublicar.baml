   M S B A M L   `   `   `  ����  ControlDeTesisV48    2ControlDeTesisV4.ProyectosCcstFolder.TesisPublicar   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��900��5      $	��750��5      $��Tesis para publicar��5      (  "Telerik.Windows.Controls.RadWindow    WindowStartupLocation$  CenterScreen=�5	      .��5
      � .+�5   
   �� $	��845��6   $	��182��5      $��21,25,-286,0q�5      $
��Left=�5      $	��Top=�5      $��Información del Oficio��5      .��5      � .+�5      �� -   5         ��Name$ TxtRefOficialia��5      $	��169��5      $��23��5      $��
171,10,0,0q�5      $
��Left=�5      $	��Top=�5      ��5      �� *
Referencia+ �� Mode$ TwoWay=��� 5      $��8,47,0,0q�6   $
��Left=�5      $	��Top=�5      $��Oficio de Recepción:��5      �� 5      -   5        $ TxtOficioRecp��5      $	��169��5       $��23��5!      $��
171,47,0,0q�5"      $
��Left=�5#      $	��Top=�5$      ��5%      �� *OficioRecepcion+$ TwoWay=�(  "System.Windows.Controls.DatePicker  5'      -   5          Name$ 	DtpFRecep��5'      $��
594,10,0,0q�5(      $
��Left=�5)      $	��Top=�5*         SelectedDate 5+      �� *
FRecepcion+$ TwoWay=��� 5-      $��
473,10,0,0q�6   $
��Left=�5.      $	��Top=�5/      $��Fecha de Recepción:��50      �� 51      $��
10,120,0,0q�6   $
��Left=�52      $	��Top=�53      $��Área emisora��54      �� 55      -   5         ��Name$ CbxEmisores��55      $	��170��56      $��171,120,0,0q�57      $
��Left=�58      $	��Top=�59       �� DisplayMemberPath$ Descripcion��5:       �� 
IsEditable . {�.   �� SelectedValuePath$ IdDato��5>      ��5=      �� 5>      �� 5?      $��473,120,0,0q�6   $
��Left=�5@      $	��Top=�5A      $��Signatario:��5B      �� 5C      -   5        $ CbxSignatario��5C      $	��229��5D      $��594,120,0,0q�5E      $
��Left=�5F      $	��Top=�5G      $ NombreCompleto��5H       . {�.  $ IdFuncionario��5M      ��5K      �� 5M      �� 5N      $��
470,47,0,0q�6   $
��Left=�5O      $	��Top=�5P      $��Archivo Oficio:��5Q      �� 5R      -   5        $ TxtArchivoPath��5R      $	��229��5S      $��23��5T      $��
594,47,0,0q�5U      $
��Left=�5V      $	��Top=�5W      ��5Z      �� *OfRecepcionPathOrigen+$ TwoWay=��� 5\      $	��163��6   $��8,17,0,0q�5]      $
��Left=�5^      $	��Top=�5_      $%��Referencia Oficialía de Partes��5`      �� 5a      $��8,90,0,0q�6   $
��Left=�5b      $	��Top=�5c      $��
Instancia:��5d      �� 5e      -   5        $ CbxInstancia��5e      $	��169��5f      $��
171,84,0,0q�5g      $
��Left=�5h      $	��Top=�5i      $ Descripcion��5j      $ IdDato��5l      ��5k      �� 5m      �� 5n      -	   5        	 ��Name$	 BtnLoadOficioRecibido��5n      $��30��5o      $��
760,75,0,0q�5p      $
��Left=�5q      $	��Top=�5r      .��5t      �� $��22��6   $��19��5u      $6��0/ControlDeTesisV4;component/Resources/browse.png��5v      5w      �� 5x      -
   5        $	 BtnViewOficioRecibido��5x      $��27��5y      $��
795,75,0,0q�5z      $
��Left=�5{      $	��Top=�5|      {�.  .��5      �� $��17��6   $��19��5�      $9��3/ControlDeTesisV4;component/Resources/preview16.png��5�      5�      5�      5�      �� 5�   
   $	��845��6   $	��463��5�      $��
21,225,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��Información de la tesis��5�      .��5�      � .+�5�      �� $��	40,77,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Rubro:��5�      �� 5�      -   5        $ TxtRubro��5�      $	��756��5�      $	��118��5�      $��	40,98,0,0q�5�      $
��Left=�5�      $	��Top=�5�      
 �� AcceptsReturn
 .  �� TextWrapping$
 Wrap=�5�      ��5�      �� *Proyecto.Rubro+$ TwoWay=�5�      	� 5�      -   5         	�Name$ Radjuris��5�      $��
133,23,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $�� Jurisprudencia��5�      	� 5�      -   5        $ 
RadAislada��5�      $��	40,23,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��
T. Aislada��5�      �� 5�      -   5         ��Name$ LblJuris��5�      $��
252,23,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��Tipo jurisprudencia:��5�      $y�	Collapsed=�5�      �� 5�      -   5        $ CbxTipoJuris��5�      $	��216��5�      $��
374,17,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $ Descripcion��5�      $ IdDato��5�      $y�	Collapsed=�5�      ��5�      �� 5�      �� 5�      $��
40,237,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Tipo de asunto:��5�      �� 5�      -   5        $ CbxTipoAsunto��5�      $	��193��5�      $��133,231,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $ Descripcion��5�      $ IdDato��5�      ��5�      �� 5�      �� 5�      $��360,237,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Número:��5�      �� 5�      -   5        $ TxtNumAsunto��5�      $��48��5�      $��23��5�      $��411,231,0,0q�5�      $
��Left=�5�      $	��Top=�5�      ��5�      �� *Proyecto.Precedente.NumAsunto+$ TwoWay=��� 5�      $��464,237,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��/��5�      �� 5�      -   5        $ TxtAnio��5�      $��49��5�      $��23��5�      $��474,231,0,0q�5�      $
��Left=�5�      $	��Top=�5�      ��5�      �� * Proyecto.Precedente.YearAsunto+$ TwoWay=��� 5�      $��564,237,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��F. de resolución:��5�        5�      -   5        $ DtpFResolucion��5�      $	��141��5�      $��655,229,0,0q�5�      $
��Left=�5�      $	��Top=�5�       5�      �� *!Proyecto.Precedente.FResolucion+$ TwoWay=��� 5�      $��66��6   $��
40,277,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��Ponente                ��5�      �� 5�      $��506,273,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Promovente:��5�      �� 5�      -   5        $ TxtPromovente��5�      $	��218��5�      $��23��5�      $��578,270,0,0q�5�      $
��Left=�5�      $	��Top=�5�      ��5�      �� * Proyecto.Precedente.Promovente+$ TwoWay=��� 5�      $��
40,318,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Archivo:��5�      
���  ��5�      �� 5�      -   5        $ TxtProyFilePath��5�      $	��273��5�      $��23��5�      $��133,311,0,0q�5      $
��Left=�5     $	��Top=�5     
 .  J� 	AllowDrop . ��5     �� *(&Proyecto.ComparaTesis.ToFilePathOrigen+$ TwoWay=��� 5	     $��
655,23,0,0q�6   $
��Left=�5
     $	��Top=�5     $��Núm. Páginas:��5     �� 5     -   5        $ 
TxtNumPags��5     $��61��5     $��23��5     $��
735,16,0,0q�5     $
��Left=�5     $	��Top=�5     ��5     �� *Proyecto.NumPaginas+$ TwoWay=��� 5     -   5        $ CbxPonentes��5     $	��273��5     $��133,271,0,0q�5     $
��Left=�5     $	��Top=�5     $ NombreCompleto��5      . {�.  $ 	IdPonente��5      ��5     �� 5      �� 5!     -   5        $ 
CbxAbogado��5!     $	��189��5"     $��
607,60,0,0q�5#     $
��Left=�5$     $	��Top=�5%     $ NombreCompleto��5&     $ IdFuncionario��5(     ��5'     �� 5(     �� 5)     $��
474,66,0,0q�6   $
��Left=�5*     $	��Top=�5+     $��Abogado Responsable:��5,     �� 5-     -   5        $	 BtnLoadPath��5-     $��30��5.     $��411,313,0,0q�5/     $
��Left=�50     $	��Top=�51     .��53     �� $��22��6   $��19��54     $6��0/ControlDeTesisV4;component/Resources/browse.png��55     56     (  "Telerik.Windows.Controls.RadButton  57     -   5          Name$ BtnGuardarTurnar��57  $   $	��106��58  $   $��32��59  $   $��690,391,0,0q�5:  $   $
��Left=�5;  $   $	��Top=�5<  $   $��Guardar y Turnar��5>  $   �� 5?     -   5         ��Name$ ChkEjecutoria��5?     $��95��5@     $��
40,361,0,0q�5A     $
��Left=�5B     $	��Top=�5C     $��  ¿ Ejecutoria ?��5E     5F     5H     5I     �� 5J  
   -   5        $	 BtnEjecutoria��5J     $��94��5K     $��23��5L     $��191,608,0,0q�5M     $
��Left=�5N     $	��Top=�5O     $��
Ejecutoria��5Q     5S     5T     