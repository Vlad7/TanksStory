   M S B A M L   `   `   `  ����J  FControlsLibrary, Version=1.0.1.1, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:ControlsLibrary;assembly=ControlsLibraryControlsLibrary  5       
TanksStory/   )TanksStory.View.Screens.SelectGame_Screen   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Gcust6clr-namespace:ControlsLibrary;assembly=ControlsLibrary     5        N� Title$  SelectGame_Screen��5	       N� 
Background 5
      �� 5   
    �� ImageSource$@ :/TanksStory;component/Resources/Images/PaperBackground.jpg��6    � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers	 P� Property
 P� Value :� RoutedEvent �� Source��%�  '��       �� 5   
   " ��6   .�5      ��  � �� �	#FFF1DE776+   �� 5       � ��$ Times New Roman)�6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   
���   �$��2q�6_   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5       5      P� 5      	 �@J�IsMouseOver
 . . �5      ��  � ��
 � ���63   5      :� 5      $ PreviewMouseDown��6   .[�5      �� $; 5/TanksStory;component/Resources/Sounds/ButtonDown.wav?�6(   5       :� 5!      $ PreviewMouseUp��6   .[�5"      �� $9 3/TanksStory;component/Resources/Sounds/ButtonUp.wav?�6(   5#      :� 5$      $ KeyUp��6   .[�5%      �� $9 3/TanksStory;component/Resources/Sounds/ButtonUp.wav?�6(   5&      :� 5'      $ KeyDown��6   .[�5(      �� $; 5/TanksStory;component/Resources/Sounds/ButtonDown.wav?�6(   5)      5/      50      .��53      �  � RowDefinitions 54   
   �� 55      $
��auto �6   6,   �� 56      $��* �6   6)   �� 57      $
��auto �6   6,    � ColumnDefinitions 59   
   �� 5:      $��* �6   6+   �� 5;      $
��auto �6   6.   �� 5<      $��* �6   6+   .+�5>   
   "   ControlsLibrary.CarouselList  -   5        $��CarouselMenu��5>         ScaleKeficient$	 1.2Q�60      AnimationTime$	 300��6E      MarginBetweenItems$	 100Q�6Y   $��1��6s   $��0��6�   $��3��6�   $��0,0,0,10q�6�   6�   �� 5@   
   $��0��6   $��0��6   $��3��6-   $	��Top=�6A   $��Center=�6Y   $��20,20,20,30q�6v   $��72(�6�   $��Times New Roman)�6�   
���   �.��6�   Choose your story!5B      �� 5C   
   -   5        $��btnPlay��5C      $
��PLAY��6    $��2q�60   $��2��6E   $��1��6R   $	��400��6b   $��70��6n   $��30,20,20,30q�6z   $��24(�6�   $��Center=�6�   $��Bottom=�6�   $��Times New Roman)�6�   
���   �5E      �� 5F   
   -   5        $��btnExit��5F      $��X��6    $��2��6,   $��0��6<   $��Right=�6I   $��60��6e   $��	0,20,20,0q�6q   $	��Top=�6�   $	��100��6�   $��30(�6�   6�   5G      5H      