   M S B A M L   `   `   `  ����  ControlDeTesisV4@    :ControlDeTesisV4.ProyectosCcstFolder.CapturaAprobacionCcst   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��843��5      $	��577��5      
�������$ ��Datos de la tesis aprobada��5      .��5
      � .+�5   
   (  "System.Windows.Controls.DatePicker  -   5           Name$  DtpFAprobacion��5      $��
147,50,0,0q�5      $
��Left=�5      $	��Top=�5         SelectedDate 5      �� *FAprobacion+ �� Mode$ TwoWay=��� 5   
   $��	38,54,0,0q�6   $
��Left=�5      $	��Top=�5      $��F. de aprobación��5      �� 5   
   $��
38,105,0,0q�6   $
��Left=�5      $	��Top=�5      $��Núm. Tesis��5      �� 5   
   -   5         ��Name$ TxtNumTesis��5      $��72��5      $��23��5      $��
147,98,0,0q�5      $
��Left=�5      $	��Top=�5       �� TextWrapping$
 Wrap=�5!      ��5      �� *
NumTesis+$ TwoWay=�5!      �� 5"   
   $��295,105,0,0q�6   $
��Left=�5#      $	��Top=�5$      $��
Año tesis��5%      �� 5&   
   -   5        $ TxtYearTesis��5&      $��72��5'      $��23��5(      $��
387,98,0,0q�5)      $
��Left=�5*      $	��Top=�5+      $
 Wrap=�5/      ��5-      �� *	YearTesis+$ TwoWay=�5/      �� 50   
   $��540,105,0,0q�6   $
��Left=�51      $	��Top=�52      $��Clave Tesis��53      �� 54   
   -   5        $ TxtClaveTesis��54      $	��174��55      $��23��56      $��
635,98,0,0q�57      $
��Left=�58      $	��Top=�59      $
 Wrap=�5<      ��5:      �� *
ClaveTesis+$ TwoWay=�5<      �� 5=   
   $��
29,165,0,0q�6   $
��Left=�5>      $	��Top=�5?      $��Archivo:��5@      
���  ��5A      �� 5B   
   -   5        $ TxtProyFilePath��5B      $	��273��5C      $��23��5D      $��124,165,0,0q�5E      $
��Left=�5F      $	��Top=�5G       �� AcceptsReturn .  J� 	AllowDrop . ��5L      �� *#!ComparaTesis.TAprobFilePathOrigen+$ TwoWay=�5N      �� 5O   
   -   5         ��Name$ BtnLoadFilePath��5O      $��30��5P      $��401,166,0,0q�5Q      $
��Left=�5R      $	��Top=�5S      .��5U      �� $��22��6   $��19��5V      $6��0/ControlDeTesisV4;component/Resources/browse.png��5W      5X      �� 5Y   
   -   5         ��Name$ TxtVistaPrevia��5Y      $	��778��5Z      $	��243��5[      $��
31,226,0,0q�5\      $
��Left=�5]      $	��Top=�5^       . 	 �� 
IsReadOnly	 . 5`      �� 5a   
   $��
31,205,0,0q�6   $
��Left=�5b      $	��Top=�5c      $��Vista previa:��5d      (  "Telerik.Windows.Controls.RadButton  5e   
   -	   5        
  Name$
 
BtnGuardar��5e      $��97��5f      $��38��5g      $��712,497,0,0q�5h      $
��Left=�5i      $	��Top=�5j      $��Guardar��5l      5n      5o      