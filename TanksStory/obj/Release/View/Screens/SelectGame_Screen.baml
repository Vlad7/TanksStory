   M S B A M L   `   `   `  ���� J  FControlsLibrary, Version=1.0.1.1, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:ControlsLibrary;assembly=ControlsLibraryControlsLibrary   
TanksStory/   )TanksStory.View.Screens.SelectGame_Screen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Gcust6clr-namespace:ControlsLibrary;assembly=ControlsLibrary       N� Title$  SelectGame_Screen�� N� 
Background ��  �� ImageSource$@ :/TanksStory;component/Resources/Images/PaperBackground.jpg�� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers	 P� Property
 P� Value :� RoutedEvent �� Source��%\  '��       �� " ��.���  � �� �	#FFF1DE77��  � ��$ Times New Roman)���  � �� �� " ��.R��� #��z��
���   �$��2q�._��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ���  � ��
 � ���:� $ PreviewMouseDown��.[��� $; 5/TanksStory;component/Resources/Sounds/ButtonDown.wav?�:� $ PreviewMouseUp��.[��� $9 3/TanksStory;component/Resources/Sounds/ButtonUp.wav?�:� $ KeyUp��.[��� $9 3/TanksStory;component/Resources/Sounds/ButtonUp.wav?�:� $ KeyDown��.[��� $; 5/TanksStory;component/Resources/Sounds/ButtonDown.wav?�.���  � RowDefinitions �� $
��auto ��� $��* ��� $
��auto � � ColumnDefinitions �� $��* ��� $
��auto ��� $��* �.+�"   ControlsLibrary.CarouselList  -   $��CarouselMenu��   ScaleKeficient$	 1.2Q�   AnimationTime$	 300��   MarginBetweenItems$	 100Q�$��1��$��0��$��3��$��0,0,0,10q��� $��0��$��0��$��3��$	��Top=�$��Center=�$��20,20,20,30q�$��72(�$��Times New Roman)�
���   �.��Choose your story!�� -   $��btnPlay��$
��PLAY��$��2q�$��2��$��1��$	��400��$��70��$��30,20,20,30q�$��24(�$��Center=�$��Bottom=�$��Times New Roman)�
���   ��� -   $��btnExit��$��X��$��2��$��0��$��Right=�$��60��$��	0,20,20,0q�$	��Top=�$	��100��$��30(�