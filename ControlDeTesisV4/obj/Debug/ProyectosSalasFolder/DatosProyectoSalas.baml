   M S B A M L   `   `   `  ����  ControlDeTesisV4>    8ControlDeTesisV4.ProyectosSalasFolder.DatosProyectoSalas   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��843��5      $	��660��5      $��DatosProyecto��5      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5	      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5
      .��5      � .+�5   
   �� $��	40,77,0,0q�6   $
��Left=�5      $	��Top=�5      $��Rubro:��5      �� 5   
   -   5         ��Name$ TxtRubro��5      $	��756��5      $	��118��5      $��	40,98,0,0q�5      $
��Left=�5      $	��Top=�5       �� AcceptsReturn .  �� TextWrapping$
 Wrap=�5      ��5      �� *Rubro+ �� Mode$ TwoWay=�5      	� 5   
   -   5         	�Name$ Radjuris��5      $��
133,23,0,0q�5      $
��Left=�5      $	��Top=�5      $�� Jurisprudencia��5       	� 5!   
   -   5        $ 
RadAislada��5!      $��	40,23,0,0q�5"      $
��Left=�5#      $	��Top=�5$      $��
T. Aislada��5&      �� 5'   
   -   5         ��Name$ LblJuris��5'      $��
284,23,0,0q�5(      $
��Left=�5)      $	��Top=�5*      $��Tipo jurisprudencia:��5+      $y�Hidden=�5,      �� 5-   
   -   5         ��Name$ CbxTipoJuris��5-      $	��216��5.      $��
406,17,0,0q�5/      $
��Left=�50      $	��Top=�51      	 �� DisplayMemberPath$	 Descripcion��52      
 �� SelectedValuePath$
 IdDato��54      $y�Hidden=�55      ��53      �� 55      �� 56   
   $��
40,237,0,0q�6   $
��Left=�57      $	��Top=�58      $��Tipo de asunto:��59      �� 5:   
   -   5        $ CbxTipoAsunto��5:      $	��193��5;      $��133,231,0,0q�5<      $
��Left=�5=      $	��Top=�5>      $	 Descripcion��5?      $
 IdDato��5A      ��5@      �� 5A      �� 5B   
   $��360,237,0,0q�6   $
��Left=�5C      $	��Top=�5D      $��Número:��5E      �� 5F   
   -   5        $ TxtNumAsunto��5F      $��48��5G      $��23��5H      $��411,231,0,0q�5I      $
��Left=�5J      $	��Top=�5K      ��5M      �� *Precedente.NumAsunto+$ TwoWay=��� 5O   
   $��464,237,0,0q�6   $
��Left=�5P      $	��Top=�5Q      $��/��5R      �� 5S   
   -	   5        $ TxtAnio��5S      $��49��5T      $��23��5U      $��474,231,0,0q�5V      $
��Left=�5W      $	��Top=�5X      ��5Z      �� *Precedente.YearAsunto+$ TwoWay=��� 5\   
   $��564,237,0,0q�6   $
��Left=�5]      $	��Top=�5^      $��F. de resolución:��5_      (  "System.Windows.Controls.DatePicker  5`   
   -
   5          Name$ DtpFResolucion��5`      $	��141��5a      $��655,229,0,0q�5b      $
��Left=�5c      $	��Top=�5d         SelectedDate 5e      �� *Precedente.FResolucion+$ TwoWay=��� 5g   
   $��66��6   $��
40,277,0,0q�5h      $
��Left=�5i      $	��Top=�5j      $��Ponente                ��5k      �� 5l   
   $��506,273,0,0q�6   $
��Left=�5m      $	��Top=�5n      $��Promovente:��5o      �� 5p   
   -   5        $ TxtPromovente��5p      $	��218��5q      $��23��5r      $��578,270,0,0q�5s      $
��Left=�5t      $	��Top=�5u      ��5v      �� *Precedente.Promovente+$ TwoWay=��� 5x   
   -   5         ��Name$ 
BtnGuardar��5x      $��75��5y      $��31��5z      $��	0,0,41,10q�5{      $��Right=�5|      $��Bottom=�5}      $��Añadir��5      �� 5�   
   $��
40,318,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Archivo:��5�      
���  ��5�      �� 5�   
   -   5        $ TxtProyFilePath��5�      $	��273��5�      $��23��5�      $��133,311,0,0q�5�      $
��Left=�5�      $	��Top=�5�      ��5�      �� *ComparaTesis.ToFilePathOrigen+$ TwoWay=�5�      �� 5�   
   $��
655,23,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Núm. Páginas:��5�      �� 5�   
   -   5        $ 
TxtNumPags��5�      $��61��5�      $��23��5�      $��
735,16,0,0q�5�      $
��Left=�5�      $	��Top=�5�      ��5�      �� *
NumPaginas+$ TwoWay=��� 5�   
   -   5        $ CbxPonentes��5�      $	��273��5�      $��133,271,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $	 NombreCompleto��5�       �� 
IsEditable . $
 IdFuncionario��5�      ��5�      �� 5�      �� 5�   
   -   5        $ 
CbxAbogado��5�      $	��189��5�      $��
607,60,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $	 NombreCompleto��5�      $
 IdFuncionario��5�      ��5�      �� 5�      �� 5�   
   $��
474,66,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Abogado Responsable:��5�      �� 5�   
   -   5        $ BtnLoadPath��5�      $��30��5�      $��411,313,0,0q�5�      $
��Left=�5�      $	��Top=�5�      .��5�      �� $��22��6   $��19��5�      $6��0/ControlDeTesisV4;component/Resources/browse.png��5�      5�      �� 5�   
   -   5         ��Name$ TxtVistaPrevia��5�      $	��756��5�      $	��192��5�      $��
40,388,0,0q�5�      $
��Left=�5�      $	��Top=�5�       .  �� 
IsReadOnly .  5�      $  Telerik.Windows.Controls.Label  5�   
   $��
40,352,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Vista Previa��5�      �� 5�   
   -   5        $ 	BtnOficio��5�      $��89��5�      $��30��5�      $��707,317,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��Datos del oficio��5�      $y�Hidden=�5�      5�      5�      