   M S B A M L   `   `   `  ����D  @AlkoPedia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:AlkoPedia	AlkoPedia  5   	       AlkoPedia.HardDrinkWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:AlkoPedia     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$ HardDrinkWindow��5
   	   $
��1000��6!   $
��1000��6/   .��5      �  � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5   
   �� 5      $
��50px �6   �� 5      $
��60px �6   �� 5      .+�5   
   � -   5         �Name$ NavGrid��5   2      nav-grid#���   6   $
��50px��6C   $	��Top=�6Q    5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       �� 5!      .+�5#      �� $��0��6   $��2��6(   $��0��6<   $��
10px 0 0 0q�6I   $
��Left=�6]   $��Center=�6x   $��	AlkoPedia��6�     
main-label#���  6�   �� 5$      -   5        $��0��5$      $��2��6%    J� Opacity$ 0Q�69   6E   �� 5%      $��Center=�6   $��Center=�64    �� Orientation$ 
Horizontal=�6Q   $��2��6j   $��8��6z   $��0��6�   $��0 0 155px 0q�6�   .+�5&      �� -   5          
nav-button#���  5&      $��Коктейли��6A   6T   �� 5'      -   5        #���  5'      $��Пользователи��6A   6X   �� 5(      -   5        #���  5(      $"��Предупреждение��6A   6Z   5)      �� 5*      -   5         ��Name$ 
user_entry��5*   >     
user-panel#���  6   $��-10px 0 0 0q�6R   $y�Hidden=�6g   $ 
Horizontal=�6{   .+�5+      �� -   5         ��Name$ user_entry_text��5+   Q   $��	Welcome, ��6     	conf-text#���  6-   6Q   �� 5,      -	   5        $
��Exit��5,      $
��20px(�6(     main-btn#���  68   $��30px 15px 0 0q�6^   $
��40px��6u   $
��28px��6�   6�   5-      �� 5.      -
   5        $ user_nentry��5.   >   #���  6   .+�5/      �� $ 
Horizontal=�6   .+�50      �� $��Имя :��6    $
��20px��6-   $��	0 5px 0 0q�6;     nav-item#���  6N   �� 51      -   5        	 ��Name$	 cur_name��51   Y   $��5px 12px 0 0q�6     	user-name#���  65   6Y   52      �� 53      $ 
Horizontal=�6   .+�54      �� $��Пароль :��6    #���  60   G� 55      -   5        $��	cur_pword��55   `   $��5px 0 0 -2pxq�6"     user-password#���  68   6`   56      57      �� 58      -   5        
 ��Name$
 ConfBtn��58   ^   $��11��6   $��0��6'   $
��30px��64   $
��18px(�6A   $��Ok��6Q   $
��26px��6o   #���  6}    J� RenderTransformOrigin$ 
2.067,0.45&�6�   $��43 5px 7px 0q�6�   6�   59      � 5:   
    	 	Workplace#��� 	 6   $��0��63   $��1��6C    5;      �� 5<      $	��97* �6#   �� 5=      $
��151* �6#   �� 5>      $
��248* �6#   .+�5@      �� -   5        $ Search��5@      $��0��6)   $ 
Horizontal=�69   $y�Visible=�6R   $��2��6g   .+�5A      �� $��Поиск:��6   $
��Left=�6*   $
��30px��6E   $
��30px(�6S   $��10px 0 0 10pxq�6c   $��Bottom=�6z    
 main-read-text#��� 
 6�   �� 5B      -   5        $	 search_text��5B      $��2��60   $��200px��6D   $��5px 0 0 8pxq�6R   $
��Left=�6g   $
��20px��6�   $��16(�6�   $��Bottom=�6�   #���  6�   �� 5C      -   5        $
 
search_btn��5C      $
��30px��6-   $
��26px��6:   $
��18px(�6H   $��	5 0 0 5pxq�6X   $��Bottom=�6k   $��Ok��6�   #���  6�   6�   5D      �� 5E      -   5        $
 back_btn��5E   &   $��0��6   $��150px��68   $
��35px��6F   $
��20px(�6T   $��10px 0 0 5pxq�6d   $��Bottom=�6z   $
��Left=�6�   $��
Назад��6�   #���  6�   $y�Hidden=�6�   6�   �� 5F      -   5        $
 fav_btn��5F   �   $
��26px(�6   $��2��6%   $��200px��65   $
��35px��6C   $��Bottom=�6Q   $��0 0 50px 5pxq�6l   $��Right=�6�   $��Избранное��6�   #���  6�   $y�Hidden=�6  �� 5G      -   5        $
 
create_btn��5G   �   $
��26px(�6   $��2��6%   $��200px��65   $
��35px��6C   $��Bottom=�6Q   $��0 0 270px 5pxq�6l   $��Right=�6�   $��Свой коктейль��6�   #���  6�   $y�Visible=�6
  5H      � 5L   
   -   5        $ lvl_list��5L      #��� 	 6!   $��2��6E   $y�Visible=�6R    5M      �� 5N      �� 5O      �� 5P      �� 5Q       5S      �� 5T      �� 5U      �� 5V      �� 5W      .+�5Y      �� $��0��6   $��0��6)   $��3��66   .+�5Z      �� $#��Нулевой уровень��6   $��10px 20px 0 0q�63     
main-title#���  6J   �� 5[      $
��Left=�6   $��10px 10px 0 0q�67     	main-text#���  6N   $��600px��6r   $��140px��6�   $����Шо ты здесь забыл? На этом уровне находятся коктейли, которые не содержат в своем составе алкоголь, и наша редакция вообще не понимает, зачем такое нужно. Так что не кликай сюда, а спускайся ниже (желательно, на пару пунктов).��6�   5\      �� 5]      -   5        $��0��5]      $��0��6%   $
��35px��62   $��140px��6@   $
��Left=�6N   $	��Top=�6i   $��10px 20px 0 0q�6�   $ 0Q�6�   6�   �� 5_      $��0��6   $��1��6)   $��3��66   .+�5`      �� $!��Первый уровень��6   $��10px -10px 0 0q�62   #���  6J   �� 5a      $
��Left=�6   $��10px 10px 0 0q�67   #���  6N   $��600px��6r   $��150px��6�   $����Вооот, уже лучше, ты находишься на Дестком уровне. На этом уровне находятся коктейли, которые являются слабоалкогольнымии, то есть, содержат в себе минимум алкоголя. В основном здесь обитают девушки, или дети (Осуждаем распитие напитков несовершеннолетними)��6�   5b      �� 5d      -   5        $��0��5d      $��1��6%   $
��35px��62   $��140px��6@   $
��Left=�6N   $	��Top=�6j   $��
10px 0 0 0q�6�   $ 0Q�6�   6�   �� 5f      $��0��6   $��2��6)   $��3��66   $��0 -20px 0 0q�6J   .+�5g      �� $!��Второй уровень��6   $��10px 20px 0 0q�62   #���  6I   �� 5h      $
��Left=�6   $��10px 10px 0 0q�67   #���  6N   $��600px��6r   $��140px��6�   $����Тааак, ты почти у цели, остался еще один пункт вниз. На этом уровне находятся коктейли, которые являются популярными у большинства любителей по-горячее, если хочешь почувствовать расслабление - тебе сюда.��6�   5i      �� 5k      -   5        $��0��5k      $��2��6%   $��2��69   $
��35px��6F   $��140px��6T   $
��Left=�6b   
���   �$	��Top=�6�   $��10px 5px 0 0q�6�   $ 0Q�6�   6�   �� 5m      $��0��6   $��3��6)   $��3��66   .+�5n      �� $!��Третий уровень��6   $��10px -20 0 0q�62   #���  6H   �� 5o      $
��Left=�6   $��10px 10px 0 0q�67   #���  6N   $��600px��6r   $��140px��6�   $����Если ты серьезно пьешь это на постоянной основе - тебе к врачу. Остальным - добро пожаловать, вы на месте. Здесь находится изюминка этого приложения, самые страшные коктейли. Но помните, главное - знать меру.��6�   5p      �� 5q      -   5        $��0��5q      $��2��6%   $��3��69   $
��35px��6F   $��140px��6T   $
��Left=�6b   
���   �$	��Top=�6�   $��10px -10px 0 0q�6�   $ 0Q�6�   6�   5r      �� 5v   
   -   5         ��Name$ drink_scroll��5v      $��2��6-   $y�Hidden=�6:   .��5w      �� -   5        $ 
drink_list��5w        present-panel#���  6-   .+�5y      �� $ 
Horizontal=�6   $��
0 20px 0 0q�67   .+�5z      �� $��500px��6!   .+�5{      �� -   5        $ paste_title��5{   $     cocktail-title#���  69   �� 5|      -   5        $ paste_el��5|   $     paste-el#���  66   �� 5}      $��0��6%   $ 0Q�60   �� 5~      -   5        $
 	paste_btn��5~   !     	paste-btn#���  64   6[   5      �� 5�      $��30px -40px 0 0q�6!   $ 
Horizontal=�69   .+�5�      �� -   5         ��Name$ 
paste_img1��5�       $��Images/Cocktails/1_1.jpg��64     	paste-img#���  6W   $��
0 30px 0 0q�6{   �� 5�      $��Images/Cocktails/1_2.jpg��6    #���  6C   $��50px 30px 0 0q�6h   5�      5�      5�      5�      � 5�   
   -   5        $ selected_cocktail��5�      $y�Hidden=�6*   $��2��6>   #��� 	 6K    5�      �� 5�      �� 5�       5�      �� 5�      �� 5�      .+�5�      �� $��0��6   $��0��6)   .+�5�      �� -    5        $ select_name��5�      $��Маргарита��61   $
��78px(�6B   #���  6S   �� 5�      -!   5        $ select_elem��5�      $��fdasfasfasfasfasfafadsf��61   $
��36px(�6P     cocktail-elements#���  6`   5�      �� 5�      -"   5        $ 	select_id��5�      $��0��6+   $��0��66   �� 5�      -#   5        $ select_img1��5�      $��1��6)   $��0��69   $��200px��6F   $
��auto��6T   $
��Left=�6b   $��Center=�6}   $��
15px 0 0 0q�6�   �� 5�      -$   5        $ select_img2��5�      $��1��6)   $��0��69   $��200px��6F   $
��auto��6T   $��Right=�6b   $��Center=�6~   $�� 0 0 20px 0q�6�   �� 5�      -%   5        $ select_recipe��5�      $��0��6/   $��2��6?   $
��auto��6S   $��1��6`   $
��40px(�6m   $	��Top=�6}   $����В первой главе при создании первого приложения с Entity Framework мы использовали подход Code First. Этот подход очень прост и удобен. Но он также и очень гибкий. Так, вполне часто распространена ситуация, когда база данных уже имеется. И здесь опять же поможет Code First. Иногда программисты называют данный подход Code Second. Посмотрим на примере.��6�     cocktail-recipe#���  6�  �� 5�      -&   5        $ in_fav��5�      $��1��6(   $��1��68   $��Bottom=�6E   $��Right=�6`   $
��40px(�6|   $��0 0 20px 20pxq�6�   $"��Уже в избранном��6�   #���  6�   $y�Hidden=�6�   �� 5�      -'   5        $
 not_fav_btn��5�      $��1��6*   $��1��6:   $��Bottom=�6G   $��Right=�6b   $��150px��6~   $
��50px��6�   $
��36px(�6�   $��0 0 20px 20pxq�6�   $��В избранное��6�   #���  6�   6�   5�      5�      5�      