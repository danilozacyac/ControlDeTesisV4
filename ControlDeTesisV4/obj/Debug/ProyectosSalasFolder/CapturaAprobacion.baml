   M S B A M L   `   `   `  ����  ControlDeTesisV4=    7ControlDeTesisV4.ProyectosSalasFolder.CapturaAprobacion   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��843��5      $	��577��5      $ ��Datos de la tesis aprobada��5      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5	      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5
      .��5      � .+�5   
   (  "System.Windows.Controls.DatePicker  -   5          Name$ DtpFAprobacion��5      $��
147,27,0,0q�5      $
��Left=�5      $	��Top=�5         SelectedDate 5      �� *FAprobacion+ �� Mode$ TwoWay=��� 5   
   $��	38,31,0,0q�6   $
��Left=�5      $	��Top=�5      $��F. de aprobación��5      �� 5   
   $��	38,82,0,0q�6   $
��Left=�5      $	��Top=�5      $��Número de Tesis��5      �� 5   
   -   5         ��Name$ TxtNumTesis��5      $��72��5      $��23��5      $��
147,75,0,0q�5      $
��Left=�5      $	��Top=�5       �� TextWrapping$
 Wrap=�5"      ��5       �� *
NumTesis+$ TwoWay=�5"      �� 5#   
   $��
38,126,0,0q�6   $
��Left=�5$      $	��Top=�5%      $��Año de la tesis��5&      �� 5'   
   -   5        $ TxtYearTesis��5'      $��72��5(      $��23��5)      $��147,119,0,0q�5*      $
��Left=�5+      $	��Top=�5,      $
 Wrap=�50      ��5.      �� *	YearTesis+$ TwoWay=�50      �� 51   
   $��311,126,0,0q�6   $
��Left=�52      $	��Top=�53      $��Clave de la Tesis��54      �� 55   
   -   5        $ TxtClaveTesis��55      $	��174��56      $��23��57      $��402,119,0,0q�58      $
��Left=�59      $	��Top=�5:      $
 Wrap=�5=      ��5;      �� *
ClaveTesis+$ TwoWay=�5=      �� 5>   
   $��
38,171,0,0q�6   $
��Left=�5?      $	��Top=�5@      $��Archivo:��5A      
���  ��5B      �� 5C   
   -   5        $ TxtProyFilePath��5C      $	��273��5D      $��23��5E      $��124,165,0,0q�5F      $
��Left=�5G      $	��Top=�5H       �� AcceptsReturn .  J� 	AllowDrop . {�.  ��5L      �� *#!ComparaTesis.TAprobFilePathOrigen+$ TwoWay=�5N      �� 5O   
   -   5        	 ��Name$	 BtnLoadFilePath��5O      $��30��5P      $��401,166,0,0q�5Q      $
��Left=�5R      $	��Top=�5S      .��5U      �� $��22��6   $��19��5V      $6��0/ControlDeTesisV4;component/Resources/browse.png��5W      5X      �� 5Y   
   -   5        
 ��Name$
 TxtVistaPrevia��5Y      $	��778��5Z      $	��243��5[      $��
31,226,0,0q�5\      $
��Left=�5]      $	��Top=�5^       .  �� 
IsReadOnly .  5`      �� 5a   
   $��
31,205,0,0q�6   $
��Left=�5b      $	��Top=�5c      $��Vista previa:��5d      (  "Telerik.Windows.Controls.RadButton  5e   
   -	   5          Name$ 
BtnGuardar��5e      $��97��5f      $��38��5g      $��712,497,0,0q�5h      $
��Left=�5i      $	��Top=�5j      $��Guardar��5l      �� 5m   
   $��24��6   $��23��5n      $��
224,75,0,0q�5o      $
��Left=�5p      $	��Top=�5q      $>��8/ControlDeTesisV4;component/Resources/questionSquare.png��5r       � ToolTip$f `Puede ser arábigo o romano de acuerdo al tipo de tesis. Ingresar de acuerdo a la clave de tesis��5s      �� 5t   
   -
   5         ��Name$ LblMes��5t      $��
480,31,0,0q�5u      $
��Left=�5v      $	��Top=�5w      $��M. de Publicación:��5x      * 
 $Telerik.Windows.Controls.RadComboBox  5y   
   -   5          Name$ CbxMPublish��5y      $	��181��5z      $��
581,25,0,0q�5{      $
��Left=�5|      $	��Top=�5}       �� DisplayMemberPath$ Descripcion��5~       �� SelectedValuePath$ IdDato��5�      ��5      �� 5�        5�   
   -   5        $ DtpFPublicacion��5�      $	��165��5�      $��
597,71,0,0q�5�      $
��Left=�5�      $	��Top=�5�       5�      �� *FPublicacion+$ TwoWay=��� 5�   
   -   5        $ LblFPublica��5�      $��
488,75,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��F. de publicación��5�      5�      5�      