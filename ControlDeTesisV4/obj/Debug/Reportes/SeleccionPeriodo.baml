   M S B A M L   `   `   `  ����  ControlDeTesisV40    *ControlDeTesisV4.Reportes.SeleccionPeriodo   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $��346.269��5      $��390.209��5      $��Periodo de impresión��5      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5	      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5
      .��5      � .+�5   
   �� -   5         ��Name$ CbxAnio��5      $	��120��5      $��	36,47,0,0q�5      $
��Left=�5      $	��Top=�5      �� 5   
   $��	36,26,0,0q�6   $
��Left=�5      $	��Top=�5      $��Selecciona el año:��5      $��14(�5      
���   �5      	� 5   
   -   5         	�Name$
 Rad1��5      $��
36,114,0,0q�5      $
��Left=�5      $	��Top=�5      $�� Enero��5      
���   �5      	� 5   
   -   5        $
 Rad2��5      $��
36,150,0,0q�5      $
��Left=�5       $	��Top=�5!      $��Febrero��5#      
���   �5$      	� 5%   
   -   5        $
 Rad3��5%      $��
36,189,0,0q�5&      $
��Left=�5'      $	��Top=�5(      $�� Marzo��5*      
���   �5+      	� 5,   
   -   5        $
 Rad5��5,      $��139,114,0,0q�5-      $
��Left=�5.      $	��Top=�5/      $
��Mayo��51      
���   �52      	� 53   
   -   5        $
 Rad6��53      $��139,150,0,0q�54      $
��Left=�55      $	��Top=�56      $��Junio��58      
���   �59      	� 5:   
   -   5        $
 Rad7��5:      $��139,189,0,0q�5;      $
��Left=�5<      $	��Top=�5=      $��Julio��5?      
���   �5@      	� 5A   
   -	   5        $
 Rad9��5A      $��232,114,0,0q�5B      $
��Left=�5C      $	��Top=�5D      $�� Septiembre��5F      
���   �5G      	� 5H   
   -
   5        $ Rad10��5H      $��232,150,0,0q�5I      $
��Left=�5J      $	��Top=�5K      $�� Octubre��5M      
���   �5N      	� 5O   
   -   5        $ Rad11��5O      $��232,189,0,0q�5P      $
��Left=�5Q      $	��Top=�5R      $��
 Noviembre��5T      
���   �5U      	� 5V   
   -   5        $
 Rad4��5V      $��
36,229,0,0q�5W      $
��Left=�5X      $	��Top=�5Y      $��Abril��5[      
���   �5\      	� 5]   
   -   5        $
 Rad8��5]      $��139,229,0,0q�5^      $
��Left=�5_      $	��Top=�5`      $�� Agosto��5b      
���   �5c      	� 5d   
   -   5        $ Rad12��5d      $��232,229,0,0q�5e      $
��Left=�5f      $	��Top=�5g      $��
 Diciembre��5i      
���   �5j      	� 5k   
   -   5        $ Rad13��5k      $	��101��5l      $��19��5m      $��119,268,0,0q�5n      $
��Left=�5o      $	��Top=�5p      $�� Todo el año��5r      
���   �5s      (  "Telerik.Windows.Controls.RadButton  5t   
   -   5          Name$ BtnContinuar��5t      $��75��5u      $��27��5v      $��237,302,0,0q�5w      $
��Left=�5x      $	��Top=�5y      $��	Continuar��5{      �� 5|   
   $��	37,83,0,0q�6   $
��Left=�5}      $	��Top=�5~      $��Selecciona el mes:��5      $��14(�5�      
���   �5�        5�   
   -   5        $ BtnCancelar��5�      $��75��5�      $��27��5�      $��145,302,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $��Cancelar��5�      5�      5�      