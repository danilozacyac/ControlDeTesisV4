   M S B A M L   `   `   `  ����M  IRichTextBoxToolBar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:RichTextBoxToolBar;assembly=RichTextBoxToolBarRichTextBoxToolBar  5   	    ControlDeTesisV45   /ControlDeTesisV4.VisualComparition.SalasCompare   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Lrtp<clr-namespace:RichTextBoxToolBar;assembly=RichTextBoxToolBar     5   	   -   5        $
��Auto��5   	   $
��Auto��5   	   $��0,0,0,0q�5	   	   .��5      �   � RowDefinitions  5   
   �� 5      $��25 �6   �� 5      $��1 �6   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   +   %RichTextBoxToolBar.RichTextBoxToolBar  -   5        $��Richtool��5   !   � 5   
   $��2��6    � ColumnDefinitions 5      �� 5      -   5         �Name$ Original��5   #   $��* �63   �� 5      -   5        $ 	Revision1��5   #   $��* �64   �� 5      -   5        $ 	Revision2��5   #   $��* �64   .+�5      �� $��0��6   
���   �$��1q�5      ._�5      � .+�5       �� -   5         ��Name$ OriginalText��5    "   $��5,25,5,0q�5!   "   $��Stretch=�5"   "   $��Stretch=�5#   "    �� AcceptsReturn . ��5%   "   �� *Value+ �� ElementName$ FontSizeSlider���� 5'      $��10,4,0,0q�6   $
��Left=�5(      $	��Top=�5)      $��Texto Original��5*      5+      5,      �� 5-      $��1��6   
���   �$��1q�5/      ._�50      � .+�51      �� -   5        $ 
ObservText��51   "   $��
10,25,10,0q�52   "   $��Stretch=�53   "   $��Stretch=�54   "    . ��56   "   �� $ FontSizeSlider�� �� Path$ Value��� 58      $��12,4,0,0q�6   $
��Left=�59      $	��Top=�5:      $��Observaciones��5;      5<      5=      �� 5>      $��2��6   
���   �$��1q�5@      ._�5A      � .+�5B      �� -   5        $ 
AprobaText��5B   "   $��
10,25,10,0q�5C   "   $��Stretch=�5D   "   $��Stretch=�5E   "    . ��5G   "   �� $ FontSizeSlider��$ Value��� 5I      $��11,3,0,0q�6   $
��Left=�5J      $	��Top=�5K      $��Texto Aprobado��5L      5M      5N      5O      �� 5P   
   -	   5         ��Name$ 
SlFontSize��5P      $��3��5Q      $	��132��5R      $��	10,17,0,0q�5S      $
��Left=�5T      $	��Top=�5U       � Maximum$ 28Q�5V      	 � Minimum$	 10Q�5W      6   �� 5X   
   -
   5        
 ��Name$
 
BtnGuardar��5X      $��3��5Y      $��75��5Z      $��	0,0,10,12q�5[      $��Right=�5\      $��Bottom=�5]      $��Guardar��5^      6#   5a      5b      