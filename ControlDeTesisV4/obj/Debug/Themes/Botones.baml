   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   EstiloCerrar  �� 
TargetType �� BasedOn �� Property �� Value �� 
TargetType � 	Resources  Key g� 	BeginTime �� 
TargetName �� TargetProperty	 O� KeyTime
 O� Value ��Name  ElementoCerrar J� BitmapEffect O� 	GlowColor O� GlowSize O� Opacity �� Triggers P� Property P� Value N� EnterActions  TimelineMouseOver N� ExitActions ��Name  TimelineMouseOut �� 
TargetName  BotonNormal g� Duration J� Opacity �� RadiusX �� RadiusY �� EndPoint �� 
StartPoint � Color � Offset J� SnapsToDevicePixels  TimelinePresionado  TimelineNormal  BotonCerrar2 	 ElementoCerrar2�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %-  &	        7  7  &	 2    7  7  &	 b    �� 5      "  ��61    6   Z� .�5      ��  � �� 5      �� 5      " ��6    5	      ���� 5
      TimelineMouseOver .��5   	   U� $ 00:00:00X�6'   $ 	rectangle��6<   $> 8(UIElement.BitmapEffect).(OuterGlowBitmapEffect.Opacity)�6^   .\�5   
   �� $	 00:00:00.5000000��6   $
 1Q�6:   5   
   5   	   �� 5      TimelineMouseOut .��5   	   U� $ 00:00:00X�6'   $ 	rectangle��6<   $> 8(UIElement.BitmapEffect).(OuterGlowBitmapEffect.Opacity)�6^   .\�5   
   �� $	 00:00:00��6   $
 1Q�62   �� 5   
   $	 00:00:00.5000000��6   $
 0Q�6:   5   
   5   	   5      .R�5      � .+�5      �� $ 	rectangle��6L   #���  6   ��6:   Z�  5   	   O� 5   
   $ 	#aaaaaaaa��6    $ 3Q�66   $ 0Q�6C   5   	   5       5      P� 5       �@J�	IsFocused . 6%   P� 5       �@��IsDefaulted . 6'   P� 5        �@J�IsMouseOver .  5!   	   �� 5"   
   8��  6    5$   	   �� 5%   
   $&  TimelineMouseOut_BeginStoryboard��6   8�� 6D   . �5'   	   �� $ 	rectangle��6(    �@J�BitmapEffect 5(   
   O� 5)      $ 	#aaaaaaaa��6!   $ 3Q�67   $ 0Q�6D   5+   
   5,   	   P� 5-       � �� . 6%   P� 5.       � {� .  6%   50      52      53      �� 54      "  ��60    6   Z� .�55      ��  � �� 56      �� 57      " ��6    58      ���� 59      TimelinePresionado .��5:   	   U� $ 00:00:00X�6'   $ 00:00:00.0010000D�6<   $ 	rectangle��6X   $I C(Shape.Fill).(GradientBrush.GradientStops)[0].(GradientStop.Offset)�6z   .\�5;   
   �� $	 00:00:00��6   $
 1Q�62   5<   
   U� 5=   	   $ 00:00:00X�6'   $ 00:00:00.0010000D�6<   $ 	rectangle��6X   $I C(Shape.Fill).(GradientBrush.GradientStops)[1].(GradientStop.Offset)�6z   .\�5>   
   �� $	 00:00:00��6   $
 0Q�62   5?   
   5@   	   �� 5A      TimelineNormal .��5B   	   U� $ 00:00:00X�6'   $ 00:00:00.0010000D�6<   $ 	rectangle��6X   $I C(Shape.Fill).(GradientBrush.GradientStops)[1].(GradientStop.Offset)�6z   .\�5C   
   �� $	 00:00:00��6   $
 1Q�62   5D   
   U� 5E   	   $ 00:00:00X�6'   $ 00:00:00.0010000D�6<   $ 	rectangle��6X   $I C(Shape.Fill).(GradientBrush.GradientStops)[0].(GradientStop.Offset)�6z   .\�5F   
   �� $	 00:00:00��6   $
 0Q�62   5G   
   5H   	   5I      .R�5J      � .+�5K      �� $ 	rectangle��6K   $ 1Q�6   ���	#FF949494$ 12��61   $ 12��6>   ��5L   	   �� 5M   
   $ 
0.5,-0.125&�6   $ 	0.5,1.083&�64   .��5N      � $ 	#FFB4B4B4��6   $ 0Q�6*   � 5O      $ 	#FFFFFFFF��6   $ 1Q�6*   5P      5R   	   �� 5S      # z 6   #��z��6U   #��z��6�   ��. 6�   5T       5U      P� 5V       �@J�	IsFocused . 6%   P� 5W       �@��IsDefaulted . 6'   P� 5X       �@J�IsMouseOver . 6'   P� 5Y       � �� .  5Z   	   �� 5[   
   8��  6    5]   	   �� 5^   
   $$ TimelineNormal_BeginStoryboard��6   8�� 6B   5`   	   P� 5a       � {� .  6%   5c      5e      5f      �� 5g      "  ��61    6   Z� .�5h      ��  � �� 5i      �� 5j      " ��6   .R�5k      � .+�5l      �� #��� 	 6   ��6;   Z� 5m       5n      P� 5o       �@J�	IsFocused . 6%   P� 5p       �@��IsDefaulted . 6'   P� 5q       �@J�IsMouseOver . 6'   P� 5r       � �� . 6%   P� 5s       � {� .  6%   5u      5w      5x      5y      