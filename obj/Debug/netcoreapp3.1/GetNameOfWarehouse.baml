   M S B A M L   `   `   `  ����D  @warehouse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:warehouse	warehouse  5   	   "    warehouse.GetNameOfWarehouse   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:warehouse     5   	   -   5          � Title$4  .Создание/открытие склада��5   	   $	��300��6*   $	��450��67    � WindowStartupLocation$ CenterScreen=�6C   .��5	      � $��0,0,0,0q�6   .+�5
   
   �� $Z��TВведите имя склада, если хотите создать новый:��6   $
��Left=�6I   $��35��6d   $��	30,40,0,0q�6p   $	��Top=�6�   $	��374��6�   $��15(�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ NameOfWarehouseTextBox��5      $
��Left=�62   $��30��6M   $��
134,87,0,0q�6Y    �� TextWrapping$
 Wrap=�6m   $	��Top=�6�   $	��270��6�   $��14(�6�   �� 5   
   -   5         ��Name$ OKButton��5      $��OK��6#   $
��Left=�60   $��35��6K   $��275,221,0,0q�6W   $	��Top=�6l   $	��129��6�   $��16(�6�   $
��Bold"�6�   6�   �� 5   
   -   5         ��Name$ 	LoadLabel��5      $D��>Загрузите раннее созданный склад:��6#   $
��Left=�6O   $��35��6j   $��
30,130,0,0q�6v   $	��Top=�6�   $	��277��6�   $��15(�6�   $
��Bold"�6�   �� 5   
   -   5        $ LoadWarehouseButton��5      $#��Загрузить склад��6.   $
��Left=�6H   $��30��6c   $��134,170,0,0q�6o   $	��Top=�6�   $	��270��6�   6�   5      5      