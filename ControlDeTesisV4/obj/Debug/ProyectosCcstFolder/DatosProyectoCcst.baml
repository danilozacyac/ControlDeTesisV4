   M S B A M L   `   `   `  ����  ControlDeTesisV4<    6ControlDeTesisV4.ProyectosCcstFolder.DatosProyectoCcst   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��843��5      $	��700��5      
�������$��DatosProyectoCcst��5      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5
      .��5      � .+�5   
   �� $��	31,39,0,0q�6   $
��Left=�5      $	��Top=�5      $��Fecha de Envio:��5      (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DtFEnvio��5      $��
132,31,0,0q�5      $
��Left=�5      $	��Top=�5         SelectedDate 5      �� *FEnvio+ �� Mode$ TwoWay=��� 5   
   $��
453,39,0,0q�6   $
��Left=�5      $	��Top=�5      $��Abogado Resposable:��5      �� 5   
   -   5         ��Name$ CbxAbogados��5      $	��227��5      $��
582,33,0,0q�5      $
��Left=�5      $	��Top=�5       �� DisplayMemberPath$ NombreCompleto��5       �� SelectedValuePath$ IdFuncionario��5!      ��5       �� 5!      �� 5"   
   $��	31,92,0,0q�6   $
��Left=�5#      $	��Top=�5$      $��Oficio de Envio:��5%      �� 5&   
   -   5         ��Name$ TxtOficioEnvioPath��5&      $	��273��5'      $��23��5(      $��
132,85,0,0q�5)      $
��Left=�5*      $	��Top=�5+       �� TextWrapping$ NoWrap=�5.      ��5,      �� *OficioEnvio+$ TwoWay=�5.      �� 5/   
   $��
31,213,0,0q�6   $
��Left=�50      $	��Top=�51      $��Rubro:��52      �� 53   
   -   5        $ TxtRubro��53      $	��717��54      $	��139��55      $��
92,213,0,0q�56      $
��Left=�57      $	��Top=�58      	 �� AcceptsReturn	 . $
 Wrap=�5<      ��5:      �� *Rubro+$ TwoWay=�5<      �� 5=   
   $��
29,450,0,0q�6   $
��Left=�5>      $	��Top=�5?      $��Archivo:��5@      
���  ��5A      �� 5B   
   -   5        $ TxtProyFilePath��5B      $	��273��5C      $��23��5D      $��124,450,0,0q�5E      $
��Left=�5F      $	��Top=�5G      	 . 
 J� 	AllowDrop
 . ��5L      �� *ComparaTesis.ToFilePathOrigen+$ TwoWay=�5N      �� 5O   
   $��648,176,0,0q�6   $
��Left=�5P      $	��Top=�5Q      $��Núm. Páginas:��5R      �� 5S   
   -   5        $ 
TxtNumPags��5S      $��54��5T      $��23��5U      $��755,176,0,0q�5V      $
��Left=�5W      $	��Top=�5X      $
 Wrap=�5\      ��5Z      �� *
NumPaginas+$ TwoWay=�5\      �� 5]   
   -   5         ��Name$ 
BtnGuardar��5]      $��75��5^      $��33��5_      $��734,629,0,0q�5`      $
��Left=�5a      $	��Top=�5b      $��Guardar��5d      �� 5e   
   $��
453,92,0,0q�6   $
��Left=�5f      $	��Top=�5g      $��Archivo oficio:��5h      �� 5i   
   -	   5        $ TxtOficioPathOrigen��5i      $	��227��5j      $��23��5k      $��
582,85,0,0q�5l      $
��Left=�5m      $	��Top=�5n      $ NoWrap=�5t      ��5q      �� *OficioEnvioPathOrigen+$ TwoWay=�5t      �� 5u   
   -
   5        $ BtnLoadPath��5u      $��30��5v      $��744,113,0,0q�5w      $
��Left=�5x      $	��Top=�5y      .��5{      �� $��22��6   $��19��5|      $6��0/ControlDeTesisV4;component/Resources/browse.png��5}      5~      �� 5   
   -   5        $ 
BtnViewDoc��5      $��30��5�      $��779,113,0,0q�5�      $
��Left=�5�      $	��Top=�5�      {�.  .��5�      �� $��22��6   $��19��5�      $9��3/ControlDeTesisV4;component/Resources/preview16.png��5�      5�      �� 5�   
   -   5        $ BtnLoadFilePath��5�      $��30��5�      $��401,451,0,0q�5�      $
��Left=�5�      $	��Top=�5�      .��5�      �� $��22��6   $��19��5�      $6��0/ControlDeTesisV4;component/Resources/browse.png��5�      5�      	� 5�   
   -   5         	�Name$ Radjuris��5�      $��124,176,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $�� Jurisprudencia��5�      	� 5�   
   -   5        $ 
RadAislada��5�      $��
31,176,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��
T. Aislada��5�      �� 5�   
   $��
31,376,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Tipo de asunto:��5�      �� 5�   
   -   5        $ CbxTipoAsunto��5�      $	��193��5�      $��124,370,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $ Descripcion��5�      $ IdDato��5�      ��5�      �� 5�      �� 5�   
   $��351,376,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Número:��5�      �� 5�   
   -   5        $ TxtNumAsunto��5�      $��48��5�      $��23��5�      $��402,370,0,0q�5�      $
��Left=�5�      $	��Top=�5�      ��5�      �� *Precedente.NumAsunto+$ TwoWay=��� 5�   
   -   5        $ TxtAnio��5�      $��49��5�      $��23��5�      $��465,370,0,0q�5�      $
��Left=�5�      $	��Top=�5�      ��5�      �� *Precedente.YearAsunto+$ TwoWay=��� 5�   
   $��555,376,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��F. de resolución:��5�        5�   
   -   5        $ DtpFResolucion��5�      $	��141��5�      $��646,368,0,0q�5�      $
��Left=�5�      $	��Top=�5�       5�      �� *Precedente.FResolucion+$ TwoWay=��� 5�   
   $��66��6   $��
31,416,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��Ponente                ��5�      �� 5�   
   $��497,412,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Promovente:��5�      �� 5�   
   -   5        $ TxtPromovente��5�      $	��218��5�      $��23��5�      $��569,409,0,0q�5�      $
��Left=�5�      $	��Top=�5�      ��5�      �� *Precedente.Promovente+$ TwoWay=��� 5�   
   -   5        $ CbxPonentes��5�      $	��273��5�      $��124,410,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $ NombreCompleto��5�       �� 
IsEditable . $ IdFuncionario��5�      ��5�      �� 5�      �� 5�   
   $��452,375,0,0q�6   $
��Left=�5�      $	��Top=�5�      $	�� / ��5�      �� 5�   
   -   5        $��LblJuris��5�      $��254,176,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��Tipo jurisprudencia:��5�      $y�Visible=�5�      �� 5�   
   -   5        $ CbxTipoJuris��5�      $	��216��5�      $��376,170,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $ Descripcion��5�      $ IdDato��5�      $y�Visible=�5�      ��5�      �� 5�      �� 5�   
   -   5         ��Name$ TxtVistaPrevia��5�      $	��778��5�      $	��113��5�      $��
31,511,0,0q�5�      $
��Left=�5�      $	��Top=�5      	 . 5     �� 5  
   $��
31,490,0,0q�6   $
��Left=�5     $	��Top=�5     $��Vista previa:��5     �� 5  
   -   5        $ 
BtnLlegada��5     $	��112��5     $��28��5     $��675,455,0,0q�5	     $
��Left=�5
     $	��Top=�5     $��Datos del oficio��5     $y�Hidden=�5     5     5     