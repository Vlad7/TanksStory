   M S B A M L   `   `   `  ����  
TanksStory*    $TanksStory.View.Screens.Start_Screen   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      |�.   N� Title$  Start_Screen��5   B    +� FocusedElement 6W   ��  �� RelativeSource �� *Self+ N� 
Background 5      �� 5	   
    �� ImageSource$1 +../..//Resources/Images/IntroBackground.jpg��6    J� InputBindings 5      �� 5   
   
 �� Key$ Enter��6    �� Command 6!   �� *ClickCommand+.��5      � -   5        $��
Start_Grid��5       � UseLayoutRounding . |�. 	 � RowDefinitions	 5   
   �� 5      $��* �6   6)   �� 5      $
��Auto �6   6-   .+�5   
   �� -   5        $��imgTitle��5   1   
 J� RenderTransformOrigin$
 0.5,0.5&�6   $��Center=�6A   $.��(../..//Resources/Images/Notification.png��6_   $	��224��6�   |�. $��955,61,21,506q�6�   z�5      �� 5      -   5        scaleTransform�� �� ScaleX$ 2Q�5       �� ScaleY$ 2Q�6$    � Triggers 5      :� 5       :� RoutedEvent$ Image.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� 
TargetName$ imgTitle��6.    �� TargetProperty$ Opacity�5   !    W� From$	 1.0X�5   !   	 W� To$	 0.9X�6,    g� Duration$ 0:0:0.5D�65    g� AccelerationRatio$	 0.8Q�6H    g� DecelerationRatio$	 0.2Q�6`    g� AutoReverse .  g� RepeatBehavior$ 5��5   4   W� 5       $ scaleTransform��6.   $ ScaleX�5!   *   $	 0.8Q�6M   $	 0.2Q�6e   $
 0.95X�5"   *   $
 1.05X�66   $ 0:0:0.5D�6@    . $ Forever��5#   =   W� 5$      $ scaleTransform��6.   $ ScaleY�5%   *   $	 0.8Q�6M   $	 0.2Q�6e   $
 0.95X�5&   *   $
 1.05X�66   $ 0:0:0.5D�6@    . $ Forever��5'   =   5)      5*      5+      5-      �� 5/   
   -   5        $��lblPressBtn��5/      $	��380��6$   
���   �|�. $��Center=�6T   $��Bottom=�6q   $��36(�6�   $��Buxton Sketch)�6�   $��450,0,370,39q�6�    50      $��Press ENTER to start..��55       56      :� 57      $ Label.Loaded��6   .[�58      �� .��59      �� .��5:      W� $ lblPressBtn��6.   $ Opacity�5;   !   $ 1Q�6E   $	 1.0X�5<   !   $ 0X�6,   $ 0:0:1D�63    . $ Forever��5=   4   5>      5?      5@      5B      �� 5C   
   $
��Left=�6   $	��300��6/   |�. $��
404,26,0,0q�6M   $
��Wrap=�6a   $	��Top=�6u   $	��546��6�   $	��100(�6�   $��Times New Roman)�6�   �� 5D      $	��120(�6   $�Tanks ��6!    �� 5E      $�
     ��6    �� 5F      $�  ��6    �� 5G       � Language$ uk-ua�6   $� ��6#    �� 5H      $��90(�6   $�Story...��6    5I      5K      5L      