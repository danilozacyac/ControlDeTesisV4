   M S B A M L   `   `   `  ����M  IRichTextBoxToolBar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:RichTextBoxToolBar;assembly=RichTextBoxToolBarRichTextBoxToolBar  5   	    ControlDeTesisV44   .ControlDeTesisV4.VisualComparition.CcstCompare   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Lrtp<clr-namespace:RichTextBoxToolBar;assembly=RichTextBoxToolBar     5   	   -   5        $
��Auto��5   	   $
��Auto��5   	   .��5      �   � RowDefinitions  5   
   �� 5      $��25 �6   �� 5      $��1 �6   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   +   %RichTextBoxToolBar.RichTextBoxToolBar  -   5        $��Richtool��5   !   � 5   
   $��2��6    � ColumnDefinitions 5      �� 5      -   5         �Name$ Original��5   #   $��* �63   �� 5      -   5        $ 	Revision2��5   #   $��* �64   .+�5      �� $��0��6   
���   �$��1q�5      ._�5      � .+�5      �� -   5         ��Name$ OriginalText��5   "   $��
10,25,10,0q�5   "   $��Stretch=�5    "   $��Stretch=�5!   "    �� AcceptsReturn . ��5#   "   �� *Value+ �� ElementName$ FontSizeSlider���� 5%      $��10,4,0,0q�6   $
��Left=�5&      $	��Top=�5'      $��Texto Original��5(      5)      5*      �� 5+      $��1��6   
���   �$��1q�5-      ._�5.      � .+�5/      �� -   5        $ 
AprobaText��5/   "   $��
10,25,10,0q�50   "   $��Stretch=�51   "   $��Stretch=�52   "    . ��54   "   �� $ FontSizeSlider�� �� Path$ Value��� 56      $��11,3,0,0q�6   $
��Left=�57      $	��Top=�58      $��Texto Aprobado��59      5:      5;      5<      �� 5=   
   -   5         ��Name$ 
SlFontSize��5=      $��3��5>      $	��132��5?      $��	10,17,0,0q�5@      $
��Left=�5A      $	��Top=�5B       � Maximum$ 28Q�5C      	 � Minimum$	 10Q�5D      �� 5E   
   -   5        
 ��Name$
 
BtnGuardar��5E      $��3��5F      $��75��5G      $��	0,12,10,0q�5H      $��Right=�5I      $	��Top=�5J      $��Guardar��5K      5N      5O      