   M S B A M L   `   `   `  ����J  FTanksGameEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null`Aclr-namespace:TanksGameEngine.GameEngine;assembly=TanksGameEngineTanksGameEngine.GameEngine  5       
TanksStory0   *TanksStory.View.Screens.GameProcess_Screen   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      RmynsAclr-namespace:TanksGameEngine.GameEngine;assembly=TanksGameEngine     5      -   5        $��GameProcess_Page��5        N� Title$  GameProcess_Screen��6    N� 
Background
 �   � � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property	 P� Value
 :� RoutedEvent �� Source��%�  '��       �� 5   
   " ��6   .�5      ��  � �� �	#FFF1DE776+   �� 5       � ��$ Times New Roman)�6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   
���   �$��2q�6_   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5       5      P� 5       �@J�IsMouseOver	 . . �5      ��  � ��
 � ���63   5      :� 5      $
 PreviewMouseDown��6   .[�5      �� $; 5/TanksStory;component/Resources/Sounds/ButtonDown.wav?�6(   5      :� 5       $
 PreviewMouseUp��6   .[�5!      �� $9 3/TanksStory;component/Resources/Sounds/ButtonUp.wav?�6(   5"      :� 5#      $
 KeyUp��6   .[�5$      �� $9 3/TanksStory;component/Resources/Sounds/ButtonUp.wav?�6(   5%      :� 5&      $
 KeyDown��6   .[�5'      �� $; 5/TanksStory;component/Resources/Sounds/ButtonDown.wav?�6(   5(      5.      5/      .��50      � -   5        $
��grid��50       � ColumnDefinitions 51   
   �� 52      $��* �6   �� 53      $
��auto �6    � RowDefinitions 56   
   �� 57      $
��Auto �6   6,   �� 58      $��* �6   6)   .+�5;   
   +   %TanksGameEngine.GameEngine.GameCanvas  -   5        $��Field��5;   \   $��0��6   $��0��6'   $��2��67   $��2��6H   ��6i   ��  �� ElementName$ GameProcess_Screen�� �� Path$ ClientWidth���6�   �� $ GameProcess_Screen��$ ClientHeight�5=      �� 5@   
   -   5        $��btnClose��5@      |�.  $��1��63   $��X��6C   $��30(�6O   $��0��6]   
��� ���$��60��6~   $��80��6�   $��Right=�6�   $	��Top=�6�   5B      5C      5D      