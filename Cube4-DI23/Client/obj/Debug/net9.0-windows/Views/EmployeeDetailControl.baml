   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Client.ViewsClient.Views  5      +clr-namespace:Client.UtilsClient.Utils  5      (    "Client.Views.EmployeeDetailControl   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      ,localclr-namespace:Client.Views     5      1
localUtilsclr-namespace:Client.Utils     5        � 	Resources  5      /   )Client.Utils.BooleanToVisibilityConverter    BooleanToVisibilityConverter��%   &	          5   
   62   5      .��5      � .+�5   
   � ���	#AA000000y�6&   �� *	IsLoading+ �� 	Converter# [  .+�5      �� $��Chargement en cours...��6   
�������$��20(�6I   $��Center=�6W   $��Center=�6t   5      � 5   
   $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   $��0,0,0,20q�6?   .+�5      �� $��
← Retour��69   $	��100��6L   $��30��6X   ��6   �� *BackCommand+6X   �� 5      $��24(�66   $
��Bold"�6D   $��20,0,0,0q�6V   $��Center=�6h   ��6   �� *
FullName+6h   5      �� 5"      $��1��6   ���#DDDDDD$��1q�68    �� CornerRadius$ 5��6L    �� Padding$ 20q�6]   ._�5#      �  � ColumnDefinitions 5$      �� 5%      $
��Auto �6+   �� 5&      $��* �6+    5(      �� 5)      $
��Auto �6(   �� 5*      $
��Auto �6(   �� 5+      $
��Auto �6(   �� 5,      $
��Auto �6(   �� 5-      $
��Auto �6(   �� 5.      $
��Auto �6(   �� 5/      $��* �6(   .+�53      �� $��0��6    $��0��6-   $��Nom :��6=   $
��Bold"�6J   $��
0,10,20,10q�6\   �� 54      $��1��6    $��0��6-   $��	Prénom :��6=   $
��Bold"�6M   $��
0,10,20,10q�6_   �� 55      $��2��6    $��0��6-   $��Téléphone fixe :��6=   $
��Bold"�6U   $��
0,10,20,10q�6g   �� 56      $��3��6    $��0��6-   $��Téléphone portable :��6=   $
��Bold"�6Y   $��
0,10,20,10q�6k   �� 57      $��4��6    $��0��6-   $��Email :��6=   $
��Bold"�6L   $��
0,10,20,10q�6^   �� 58      $��5��6    $��0��6-   $��	Service :��6=   $
��Bold"�6N   $��
0,10,20,10q�6`   �� 59      $��6��6    $��0��6-   $��Site :��6=   $
��Bold"�6K   $��
0,10,20,10q�6]   �� 5<      $��0��6    $��1��6-   $��	0,10,0,10q�6v   ��6=   �� *Employee.LastName+ �� TargetNullValue$ --��6v   �� 5=      $��1��6    $��1��6-   $��	0,10,0,10q�6w   ��6=   �� *Employee.FirstName+$ --��6w   �� 5>      $��2��6    $��1��6-   $��	0,10,0,10q�6s   ��6=   �� *Employee.Phone+$ --��6s   �� 5?      $��3��6    $��1��6-   $��	0,10,0,10q�6y   ��6=   �� *Employee.MobilePhone+$ --��6y   �� 5@      $��4��6    $��1��6-   $��	0,10,0,10q�6s   ��6=   �� *Employee.Email+$ --��6s   �� 5A      $��5��6    $��1��6-   $��	0,10,0,10q�6s   ��6=   �� *DepartmentName+$ --��6s   �� 5B      $��6��6    $��1��6-   $��	0,10,0,10q�6m   ��6=   �� *
SiteCity+$ --��6m   5C      5D      5E      5F      5G      