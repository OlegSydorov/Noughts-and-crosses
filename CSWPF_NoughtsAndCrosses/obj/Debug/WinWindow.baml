   M S B A M L   `   `   `  ????R  NCSWPF_NoughtsAndCrosses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CSWPF_NoughtsAndCrossesCSWPF_NoughtsAndCrosses  5   	   '    !CSWPF_NoughtsAndCrosses.WinWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:CSWPF_NoughtsAndCrosses     5   	     ? Title$  	WinWindow??5   	   $	??250??6   $	??600??6(   
??????  ? WindowStyle$
 None=?6M    ? WindowStartupLocation$ CenterScreen=?6`   .??5	      ?  ? RowDefinitions 5
   
   ?? 5      $
??250* ?6    ? ColumnDefinitions 5   
   ?? 5      $
??600* ?6   .+?5   
   ?? -   5         ??Name$ textBox??5      
?????? $??You win!??6<   $??100, 20, 100, 100q?6L   $??Arial Black)?6g   $??60(?6?   
???  ?? ?? TextAlignment$ Center=?6?   $??Center=?6?   ???	#FFEE093D$??10,10,10,10q?6?   $??Stretch=?6  $??Stretch=?6,  ?? 5   
   -   5         ??Name$ label??5      
?????? $??Another game???68   $??60, 180, 250, 15q?6P   $??Arial Black)?6j   $??30(?6?   
???  ??$??Center=?6?   $??Center=?6?   $??Stretch=?6?   $??Stretch=?6  ?? 5   
   -   5         ??Name$
 _Yes??5      
???  ??$	??Yes??60   $??350, 180, 170, 20q?6>   $??Arial Black)?6Y   $??30(?6r   
???????$??Center=?6?   $??Center=?6?   $??Stretch=?6?   $??Stretch=?6
  ?? 5   
   -   5        $	 _No??5      
???  ??$??No??6/   $??450, 180, 70, 20q?6<   $??Arial Black)?6V   $??30(?6o   
???????$??Center=?6?   $??Center=?6?   $??Stretch=?6?   $??Stretch=?6  5      5      