   M S B A M L   `   `   `  ����   
TanksStory*    $TanksStory.View.Screens.Start_Screen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     |�.   N� Title$  Start_Screen�� +� FocusedElement ��  �� RelativeSource �� *Self+ N� 
Background ��  �� ImageSource$1 +../..//Resources/Images/IntroBackground.jpg�� J� InputBindings �� 
 �� Key$ Enter�� �� Command �� *ClickCommand+.��� -   $��
Start_Grid�� � UseLayoutRounding . |�. 	 � RowDefinitions	 �� $��* ��� $
��Auto �.+��� -   $��imgTitle��
 J� RenderTransformOrigin$
 0.5,0.5&�$��Center=�$.��(../..//Resources/Images/Notification.png��$	��224��|�. $��955,61,21,506q�z��� -   scaleTransform�� �� ScaleX$ 2Q� �� ScaleY$ 2Q� � Triggers :�  :� RoutedEvent$ Image.Loaded��.[��� .���� .��W�  �� 
TargetName$ imgTitle�� �� TargetProperty$ Opacity� W� From$	 1.0X�	 W� To$	 0.9X� g� Duration$ 0:0:0.5D� g� AccelerationRatio$	 0.8Q� g� DecelerationRatio$	 0.2Q� g� AutoReverse .  g� RepeatBehavior$ 5��W� $ scaleTransform��$ ScaleX�$	 0.8Q�$	 0.2Q�$
 0.95X�$
 1.05X�$ 0:0:0.5D� . $ Forever��W� $ scaleTransform��$ ScaleY�$	 0.8Q�$	 0.2Q�$
 0.95X�$
 1.05X�$ 0:0:0.5D� . $ Forever���� -   $��lblPressBtn��$	��380��
���   �|�. $��Center=�$��Bottom=�$��36(�$��Buxton Sketch)�$��450,0,370,39q� $��Press ENTER to start..�� :� $ Label.Loaded��.[��� .���� .��W� $ lblPressBtn��$ Opacity�$ 1Q�$	 1.0X�$ 0X�$ 0:0:1D� . $ Forever���� $
��Left=�$	��300��|�. $��
404,26,0,0q�$
��Wrap=�$	��Top=�$	��546��$	��100(�$��Times New Roman)��� $	��120(�$�Tanks �� �� $�
     �� �� $�  �� ��  � Language$ uk-ua�$� �� �� $��90(�$�Story...��