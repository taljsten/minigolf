   M S B A M L   `   `   `  ����O  KAssignment7-MiniGolf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Assignment7_MiniGolfAssignment7_MiniGolf  5   	   %    Assignment7_MiniGolf.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Assignment7_MiniGolf     5   	   -   5          �Name$  
mainWindow��5   	    � Title$ MiniGolf��5   	   $	��804��6   $	��513��6'    � 
ResizeMode$ NoResize=�63    � DataContext$
 this��5	   	   .��5
      � -   5         �Name$ TopGrid��5
      $
��Left=�6   $	��770��67   $	��Top=�6D   $	��511��6\   .+�5   
   � -   5        $ gameGrid��5       J� RenderTransformOrigin$	 0,0&�6�   $	��239��6�   $	��742��6�   $��Bottom=�6�   $
��Left=�6�   $��	10,0,0,10q�6T  ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF16250B��6#   	 � Offset$	 0Q�65   � 5      $ 	#FF3B8002��6#   $	 1Q�65   5      5      � 5   
   -   5        $ InfoGrid��5      $
��Left=�6!   $	��666��6<   $��
268,18,0,0q�6I   $	��Top=�6]   $	��218��6u   ���	#FFBDA2A2.+�5      �� -   5        
 ��Name$
 
txtMessage��5      $
��Left=�6,   $��	10,40,0,0q�6G   $
��Wrap=�6Z   $/��)Welcome, click left mouse button to begin��6n   $	��Top=�6�   $	��183��6�   $��86��6�   ���	#FF0016F3$
��Bold"�6�   �� 5      -   5         ��Name$ lblForce��5      $��Force��6&   $
��Left=�66   $��
11,0,0,111q�6Q   $��Bottom=�6e   $ 0.316,0.577&�6�   �� 5      -   5        $ 	lblPlayer��5      $��Label��6'   $
��Left=�67   $��10,9,0,0q�6R   $	��Top=�6d   $��16(�6|   �� 5      -   5         ��Name$ 	btnPlayer��5      $��Change Player��6(   $
��Left=�6@   $��	10,0,0,35q�6[   $��Bottom=�6n   $	��198��6�   6�   �� 5      -	   5        $ 
lblStrokes��5      $��Strokes��6(   $
��Left=�6:   $��
10,0,0,142q�6U   $��Bottom=�6i   �� 5      -
   5        $ lblDiff��5      $��Difficulty : 1��6%   $
��Left=�6>   $��	10,0,0,85q�6Y   $��Bottom=�6l   �� 5      -   5         ��Name$ 
sliderDiff��5      $
��Left=�6)   $��	99,0,0,85q�6D   $��Bottom=�6W   $	��109��6r    � Maximum$ 6Q�6~    � SmallChange$ 1Q�6�   6�   �� 5      -   5        $ btnReset��5      $��Reset with difficulty��6'   $
��Left=�6G   $��9,0,0,60q�6b   $��Bottom=�6t   $	��198��6�   6�   � 5      -   5         �Name$ progressForce��5      $
��Left=�61   $��10��6L   $��
99,0,0,118q�6X   $��Bottom=�6l   $	��100��6�   ���	#FFDE0909{�.  $ 10Q�6�   �� 5      -   5        $ lblScore��5      $��Score��6&   $
��Left=�66   $��
11,0,0,168q�6Q   $��Bottom=�6e   �� 5      -   5        $ lblLastScore��5      $��
Last Score��6*   $
��Left=�6?   $��	9,167,0,0q�6Z   $	��Top=�6m   �� 5      -   5        $ btnExit��5      $
��Exit��6&   $
��Left=�65   $��9,0,0,10q�6P   $��Bottom=�6b   $	��198��6}   6�   �� 5       -   5        $ lblLastStrokes��5       $��LAst Strokes��6,   $
��Left=�6C   $��	9,198,0,0q�6^   $	��Top=�6q   �� 5!      -   5        $ lblBest��5!      $��
Best Score��6%   $
��Left=�6:   $��	9,136,0,0q�6U   $	��Top=�6h   5"      � 5#   
   -   5        $ developGrid��5#      $
��Left=�6$   $��71��6?   $��268,689,0,0q�6K   $	��Top=�6`   $	��218��6x   ���	#FFC93B3B.+�5$      �� -   5        $ lblPlayerInput��5$      $��Label��6,   $
��Left=�6<   $��	10,36,0,0q�6W   $	��Top=�6j   ���	#FFF9F9F96�   �� 5%      -   5        $ lblGameState��5%      $	��xxx��6*   $
��Left=�68   $��	10,10,0,0q�6S   $	��Top=�6f   $	��100��6~   
�������6�   5&      5'      5(      