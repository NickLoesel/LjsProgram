   M S B A M L   `   `   `  ����E  ALjsProgram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:LjsProgram
LjsProgram  5   	       LjsProgram.frmAddOrder   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:LjsProgram     5   	   -   5          � Title$  frmAddOrder��5   	   $	��450��6   $	��800��6*   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $	��250 �6   6+   �� 5      6   .+�5   
   �� -   5         ��Name$ tbkOrderTitle��5      $��1��6+   $��20(�6?   $
��Bold"�5      $��Center=�6&   $��Center=�5      $��1��61   
Order Date6J   &   System.Windows.Controls.Calendar  5   
   -   5        $��orderCalender��5      $	��300��5      $
��Left=�6    $��0,32,0,0q�6;   $	��Top=�5      $	��400��61      DisplayMode$ Month=�6=   $��2��6Q   �� 5   
   -   5        $ tbkTitle��5      $��2��6&   $��25(�6:   $
��Bold"�5      $��Center=�6&   $��Center=�5      Add An Order6@   �� 5   
   -   5        $ tbkCustomer��5      $��2��6)   $��20(�69   $
��Bold"�5      $��Center=�6&   $��Center=�5      $��1��61   
Customer6H   &   System.Windows.Controls.DataGrid  5    
   -   5          Name$ dgCustomerList��5       $��1��6+   $��2��6;   $	��350��6H   $	��200��6W   6f   �� 5!   
   -   5        $��0��5!      $��3 ��6!   $	��100��6/   $��50��6;   $��200 20 20 20q�6G   .��6r   Cancel6z   �� 5"   
   -   5         ��Name$ btnConfirmOrder��5"      $��0��6*   $��3 ��6:   $	��100��6H   $��50��6T   $��10 20 200 20q�6`   .��6�   	Confirm6�   �� 5#   
   -	   5         ��Name$ 
txtBoxTime��5#      $��3��6&   $��1��63   $	��100��6C   $��20��6O   .��6z   Enter a time6�   5&      5'      