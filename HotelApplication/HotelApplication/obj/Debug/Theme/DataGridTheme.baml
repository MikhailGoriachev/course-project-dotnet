   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089=   7System.Windows.Controls.Primitives.DataGridColumnHeader  �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Padding J� SnapsToDevicePixels)  #System.Windows.Controls.DataGridRow?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter	   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter
   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter&   System.Windows.Controls.DataGrid#   RowDetailsScrollingConverter �� 	Converter �� RelativeSource �� AncestorType:  4System.Windows.Controls.Primitives.DataGridRowHeader7  1System.Windows.Controls.DataGridHeadersVisibility
   Row!   HeadersVisibilityConverter'	  !System.Windows.VisualStateManager 	  VisualStateGroups%
  System.Windows.VisualStateGroup 
  States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value*  $System.Windows.Controls.DataGridCell   BackgroundColor  StandardColor  
HoverColor  PressedColor  DialogBackgroundColor  ScollBarBackgroundStart  ScollBarBackgroundEnd  StandardBrush �� Color  
HoverBrush 	 BackgroundBrush 
 ScollBarBackgroundBrush �� 
StartPoint �� EndPoint � Offset � Color  ScrollButtons  ScrollThumbs  �� RadiusX! �� RadiusY  MyScrollBar" �Name# ��Name$ F�Name% ]�Name& ]� Thumb' n�Name( �� Triggers) P� 
SourceName* P� Property+ P� Value, �� 
TargetName- �� CornerRadius. ��Name/   SelectAllCommand0 �� 
ResourceId1 �� TypeInTargetAssembly
2   AllG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter3  Name4   Column5 ��Name6 �� CanContentScroll7 ��Name8 �� ScrollableHeight9 � Maximum: �� Orientation; �� ViewportHeight< �� ViewportSize*= �� #ComputedVerticalScrollBarVisibility> � Value? �� Mode@ �� ScrollableWidthA �� ViewportWidth,B �� %ComputedHorizontalScrollBarVisibilityC �� Triggers�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�  '         '  �    '      &	  �    &	 		    &	 (	    &	 I	    &	 h	    &	 �	    &	 �	    &	 �	    7  &	 �	    7  &		 �	    7   &	
 
    7  7  &	 �
    &	 K    7 	 &	 �    7  7  7  7  7  '  �    �� 5      "    6   .�5   
   ��  � ��$ Center=�65   �� 5   
    � ��$ 40��6#   �� 5   
    �@  SeparatorBrush
 �  ��6+   �� 5	   
    � ��
 �����6'   �� 5   
    � �� 5      �� 5      "   6"   .R�5      � .+�5      �� $ columnHeaderBorder��6!   $	��0.5q�5   !   $
 10 0q�5   !   ���#fff
������ ._�5)      �� #��z��6/   #��z��5*   /   # z 5+   /   5-      5O      5P      5Q      5S      5U      �� 5Z      "   6   .�5[   
   ��  � ��
 �����6'   �� 5\   
    � ��
 �����6'   �� 5]   
    � ��$ 15(�6%   �� 5^   
    �@=�ErrorTemplate 65   Z� �� 5a   
    � �� 5b      �� 5c      "  6"   .R�5d      �� $ 
DGR_Border��6   #��z��5e      #��z��5f       . ���#3B3853._�5k         5m      �� 5n   "   $
��auto �63   �� 5o   "   $��* �63    5r      �� 5s   "   $��30 �60   �� 5t   "   $
��auto �60   .+�5w        $��1��65   #��z	 5x   5   # z 5y   5     5{      $��1��67   $��1��5|   7   #y�z
 5�   7    5}   7   �� *AreRowDetailsFrozen+# Z # Z  �� "  5�   7     5�      $��2��60   $ Vertical=�5�   0   y�5�   0   �� *HeadersVisibility+# Z # Z  �� "  5�       5�      
  5�      CommonStates��. 5�   "     
Normal��6.     5�   "   Normal_AlternatingRow��. 5�   &   �� .��5�   *   �� $ 
DGR_Border��6G   $ Background.Color�5�   G   .g�5�   .     $ 0��6B   $ #7336FF��6N   5�   +   5�   '   5�   #     5�   "   Normal_Selected��. 5�   &   �� .��5�   *   �� $ 
DGR_Border��6G   $ Background.Color�5�   G   .g�5�   .     $ 0:0:0:1��6B   $ #9B31E8��6T   5�   +   5�   '   5�   #     5�   "   	MouseOver��. 5�   &   �� .��5�   *   �� $ 
DGR_Border��6G   $ Background.Color�5�   G   .g�5�   .     $ 0:0:0:1��6B   $ #9F3CE8��6T   5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�      "   6   .�5�   
   ��  � �� 5�      �� 5�      "  6"   .R�5�      �� $ border��6   
������ 
������ $��1q�5�       . ._�5�      �� # z 6+   5�      5�      5�      5�      ��5�      #331873��6-   ��5�      #E342FF��6+   ��5�      	#AAC64D45��6*   ��5�      #E342FF��6*   ��5�      	#FF666666��65   ��5�      #5C1873��65   ��5�      #5C1873��63   �� 5�      8   6,   �� 5�      8   6)   �� 5�      8   6.   �� 5�      $	 0,0&�6:   $	 1,0&�6K   ��5�      � $ 0Q�6   8   6&   � 5�      $ 1Q�6   8  6&   5�      �� 5�      "  ��6"   .�5�   
   ��  � �� 5�      �� 5�      " ��6"   .R�5�      �� $��Border��6   
������ ._�5�      �� 5�      5�      5�      5�      �� 5�      "  n�6!   .�5�   
   ��  � ��8   6'   �� 5�   
    � �� 5�      �� 5�      " n�6"   .R�5�      � .+�5�      �� $��
Rectangle1��6$   $��7��5�   $   $
��auto��5   $   $��Stretch=�5  $   #��z��5  $   $  4��5  $   $! 4��5  $   5     5     5     5	     �� 5     "  ��6    .�5  
   ��  � ��# � 
 6'   �� 5  
    � ��$ 12��6"   �� 5  
    � �� 5     �� 5     " ��6"   .R�5     � $" GridRoot��6   #��� ��5     #��z��5      5     �� 5     $��18��6,   �� 5     $��0.00001* �6,   �� 5     $��18��6,   .+�5     �� $# DecreaseRepeat��6'   $��ScrollBar.LineUpCommand��5  '   8��  #���  5  '   .��5     F� $$ DecreaseArrow��6#   $��Center=�5   #   $��Center=�5!  #   +�����g5     �    ��j ���j ��j ��g5     8�� 5#  #   5$     ]� 5&     $% 
PART_Track��6    $��1��5'      |�.  ��. & 5+     n� 5,  "   $' Thumb��6(   
�������#���  5.  (   50     �� 52     $# IncreaseRepeat��6'   $��2��53  '   $��ScrollBar.LineDownCommand��54  '   #���  55  '   #���  56  '   .��57     F� $$ IncreaseArrow��6#   $��Center=�58  #   $��Center=�59  #   +�����g5 ��j ���j     �        ��g5 ��j 8�� 5;  #   5<     5=     ( 5?     P� 5@     $) IncreaseRepeat��6"   * �@J�IsMouseOver+ . . �5A     �� $, IncreaseArrow��6%   8   � ��5B  %   5C     P� 5E     $) Thumb��6"   * �@n�
IsDragging+ . . �5F     �� $, Thumb��6%   8   � ��5G  %   5H     5J     5L     5M     �� 5O     "   6   .�5P  
   ��  � ��# � ��6'   �� 5Q  
     �@ RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5R  
    � �� . 6:   �� 5S  
    �@��PanningMode$
 Both=�65   �� 5T  
    �@��IsFlicksEnabled .  63   �� 5U  
    � �� 5V     �� 5W     "  6"   .R�5X     �� #��z��6   #��z��5Y     $��2q�5Z     $- 5��5[     # z��5\      . ._�5^     �� $. DG_ScrollViewer��6'   |�.  ��5_     �� 5`  "   " ��62   .R�5a  &   �  5b  *   �� 5c  .   $
��Auto �6?   �� 5d  .   $��* �6?   �� 5e  .   $
��Auto �6?    5g  *   �� 5h  .   $
��Auto �6<   �� 5i  .   $��* �6<   �� 5j  .   $
��Auto �6<   .+�5l  *   �� #��Z/ 5n     |�.  ��5l  1   �� *CellsPanelHorizontalOffset+ �� "  ��5p     C� *�� $"0 DataGridSelectAllButtonStyle��"1  +y�5r     �� *HeadersVisibility+# Z2 # Z  �� "    5v  *   $!3 PART_ColumnHeadersPresenter��6I   $��1��5w  +   y�5x  +   �� *HeadersVisibility+# Z4 # Z  �� "  �� 5|  *   $!5 PART_ScrollContentPresenter��6A   $��1��5}  #   $��2��5~  #   #6 z��5  #   �� 5�  *   $7 PART_VerticalScrollBar��64   $��1��5�     $��2��5�     #9 z8 5�     $: Vertical=�5�     #���  5�     #< z; 5�     #y�z= 5�     > 5�     �� *VerticalOffset+$? OneWay=� �� *TemplatedParent+� 5�  *   $��2��6/   $��1��6<    5�  .   �� 5�  2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "  �� 5�  2   $��* �6C   .+�5�  .   �� $7 PART_HorizontalScrollBar��68   $��1��5�     #9 z@ 5�     $: 
Horizontal=�5�     #< zA 5�     #y�zB 5�     > 5�     �� *HorizontalOffset+$? OneWay=� �� *TemplatedParent+5�  +   5�  '   5�  #   .��5�     �� # z 6-   5�     5�     5�     5�     C 5�  
   P� 5�     * �@��
IsGrouping+ . . �5�     ��  � �� . 6B   5�     5�     5�     