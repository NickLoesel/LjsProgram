   M S B A M L   `   `   `  ����E  ALjsProgram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:LjsProgram
LjsProgram  5   	   #    LjsProgram.frmAddEditCustomer   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:LjsProgram     5   	   -   5          � Title$  frmAddEditCustomer��5   	   $	��450��6$   $	��800��61   .��5	      �  � RowDefinitions 5
   
   �� 5      $��45 �6   6*   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      $��30 �6   6+   �� 5      $��30 �6   6+   �� 5      $��30 �6   6+   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      $��50 �6   6*    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   �� -   5         ��Name$ txtBlockTitle��5      $��2��6+   $��20(�6?   $
��Bold"�5      $��Center=�6&   $��Center=�5      
CustomerID6=   �� 5   
   $��Center=�6   $��0 0 10 0q�6+   $��Right=�5      $��1��6,   $��15(�69   .��6G   Customer ID:6U   �� 5    
   -   5         ��Name$ txtCustomerID��5        �� 
IsReadOnly . 
�������$��1��5!      $��1��6   $��5q�6/   $
��Left=�6:   $��Center=�5"      $��15(�64   .��6B   Text6H   �� 5#   
   $��Center=�6   $��0 0 10 0q�6+   $��Right=�5$      $��2��6,   $��15(�69   .��6G   Business Name:6W   �� 5%   
   -   5        $ txtBusinessName��5%       . 
�������$��2��6Q   $��1��5&      $��	5 5 250 5q�6"   $
��Left=�65   $��Center=�5'      $��15(�64   .��6B   Text6H   �� 5(   
   $��Center=�6   $��0 0 10 0q�6+   $��Right=�6>   $��3��6Z   $��15(�6g   .��6u   Customer First Name:6�   �� 5)   
   -   5        $ txtFirstName��5)       . 
�������$��3��6N   $��1��6[   $��	5 5 250 5q�6k   $
��Left=�6~   $��Center=�6�   $��15(�6�   .��6�   Text6�   �� 5*   
   $��Center=�6   $��0 0 10 0q�6+   $��Right=�6>   $��4��6Z   $��15(�6g   .��6u   Customer Last Name:6�   �� 5+   
   -   5        $ txtLastName��5+       . 
�������$��4��6M   $��1��6Z   $��	5 5 250 5q�6j   $
��Left=�6}   $��Center=�6�   $��15(�6�   .��6�   Text6�   �� 5,   
   $��Center=�6   $��0 0 10 0q�6+   $��Right=�6>   $��5��6Z   $��15(�6g   .��6u   Customer Email:6�   �� 5-   
   -   5        $ txtCustomerEmail��5-       . 
�������$��5��6R   $��1��6_   $��	5 5 250 5q�6o   $
��Left=�6�   $��Center=�6�   $��15(�6�   .��6�   Text6�   �� 5.   
   $��Center=�6   $��0 0 10 0q�6+   $��Right=�6>   $��6��6Z   $��15(�6g   .��6u   Customer Phone Number:6�   �� 5/   
   -   5        $ txtCustomerPhoneNumber��5/       . 
�������$��6��6X   $��1��6e   $��	5 5 250 5q�6u   $
��Left=�6�   $��Center=�6�   $��15(�6�   .��6�   Text6�   �� 52   
   $��Center=�6   $��0 0 10 0q�6+   $��Right=�53      $��7��54      $��15(�6   .��6+   	Active:64   �� 55   
   -	   5         ��Name$ chkBoxActive��55      $��	5 5 250 5q�6)   $��7��6<   $��1��56      $
��Left=�6   $��Center=�6:   {�.  6U   �� 57   
   -
   5         ��Name$ btnEditSaveCustomer��57      $��5 5 10 5q�6.   $��Right=�58      $	��100��6,   $��9��59      $��15(�6   .��6M   Edit6S   �� 5:   
   -   5        $ 	btnCancel��5:      $��5 5 10 5q�6$   $
��Left=�5;      $	��100��6+   $��9��5<      $��1��6   $��15(�6-   .��6<   Cancel6D   5=      5>      