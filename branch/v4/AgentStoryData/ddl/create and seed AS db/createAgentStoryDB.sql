- -   c r e a t e   t h e   A g e n t S t o r y E v o l u t i o n   D B  
  
 U S E   [ m a s t e r ]  
 G O  
 / * * * * * *   O b j e c t :     D a t a b a s e   [ A g e n t S t o r y E v o l u t i o n ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 2   * * * * * * /  
 I F   N O T   E X I S T S   ( S E L E C T   n a m e   F R O M   s y s . d a t a b a s e s   W H E R E   n a m e   =   N ' A g e n t S t o r y E v o l u t i o n ' )  
 B E G I N  
 C R E A T E   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   O N     P R I M A R Y    
 (   N A M E   =   N ' A g e n t S t o r y E v o l u t i o n ' ,   F I L E N A M E   =   N ' C : \ D a t a \ M S S Q L \ G a m e S t o r y \ A g e n t S t o r y E v o l u t i o n . m d f '   ,   S I Z E   =   1 3 5 0 4 K B   ,   M A X S I Z E   =   U N L I M I T E D ,   F I L E G R O W T H   =   1 0 2 4 K B   )  
   L O G   O N    
 (   N A M E   =   N ' A g e n t S t o r y E v o l u t i o n _ l o g ' ,   F I L E N A M E   =   N ' C : \ D a t a \ M S S Q L \ G a m e S t o r y \ A g e n t S t o r y E v o l u t i o n _ l o g . L D F '   ,   S I Z E   =   5 0 4 K B   ,   M A X S I Z E   =   U N L I M I T E D ,   F I L E G R O W T H   =   1 0 % )  
 E N D  
  
 G O  
 E X E C   d b o . s p _ d b c m p t l e v e l   @ d b n a m e = N ' A g e n t S t o r y E v o l u t i o n ' ,   @ n e w _ c m p t l e v e l = 9 0  
 G O  
 I F   ( 1   =   F U L L T E X T S E R V I C E P R O P E R T Y ( ' I s F u l l T e x t I n s t a l l e d ' ) ) 
 b e g i n 
 E X E C   [ A g e n t S t o r y E v o l u t i o n ] . [ d b o ] . [ s p _ f u l l t e x t _ d a t a b a s e ]   @ a c t i o n   =   ' e n a b l e ' 
 e n d  
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A N S I _ N U L L _ D E F A U L T   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A N S I _ N U L L S   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A N S I _ P A D D I N G   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A N S I _ W A R N I N G S   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A R I T H A B O R T   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A U T O _ C L O S E   O N    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A U T O _ C R E A T E _ S T A T I S T I C S   O N    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A U T O _ S H R I N K   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A U T O _ U P D A T E _ S T A T I S T I C S   O N    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   C U R S O R _ C L O S E _ O N _ C O M M I T   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   C U R S O R _ D E F A U L T     G L O B A L    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   C O N C A T _ N U L L _ Y I E L D S _ N U L L   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   N U M E R I C _ R O U N D A B O R T   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   Q U O T E D _ I D E N T I F I E R   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   R E C U R S I V E _ T R I G G E R S   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T     D I S A B L E _ B R O K E R    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A U T O _ U P D A T E _ S T A T I S T I C S _ A S Y N C   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   D A T E _ C O R R E L A T I O N _ O P T I M I Z A T I O N   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   T R U S T W O R T H Y   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   A L L O W _ S N A P S H O T _ I S O L A T I O N   O F F    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   P A R A M E T E R I Z A T I O N   S I M P L E    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T     R E A D _ W R I T E    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   R E C O V E R Y   S I M P L E    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T     M U L T I _ U S E R    
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   P A G E _ V E R I F Y   C H E C K S U M      
 G O  
 A L T E R   D A T A B A S E   [ A g e n t S t o r y E v o l u t i o n ]   S E T   D B _ C H A I N I N G   O F F    
 U S E   [ A g e n t S t o r y E v o l u t i o n ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e E l e m e n t T y p e ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t T y p e ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e E l e m e n t T y p e ] (  
 	 [ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ c o d e ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ n a m e ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e E l e m e n t T y p e ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ W o r d s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ W o r d s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ W o r d s ] (  
 	 [ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ w o r d ]   [ n v a r c h a r ] ( 5 0 )   N O T   N U L L  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ U s e r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ U s e r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ U s e r s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ f i r s t N a m e ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ l a s t N a m e ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ u s e r n a m e ]   [ n v a r c h a r ] ( 5 0 )   N O T   N U L L ,  
 	 [ p a s s w o r d ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ n i c k ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ e m a i l ]   [ n v a r c h a r ] ( 1 2 8 )   N U L L ,  
 	 [ r o l e s ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ s t a t e ]   [ t i n y i n t ]   N O T   N U L L ,  
 	 [ O r i g I n v i t a t i o n C o d e ]   [ n v a r c h a r ] ( 5 0 )   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ d a t e A c t i v a t e d ]   [ d a t e t i m e ]   N U L L ,  
 	 [ d a t e L a s t A c t i v e ]   [ d a t e t i m e ]   N U L L ,  
 	 [ p e n d i n g G U I D ]   [ u n i q u e i d e n t i f i e r ]   N U L L ,  
 	 [ u s e r G U I D ]   [ u n i q u e i d e n t i f i e r ]   N O T   N U L L   C O N S T R A I N T   [ D F _ U s e r s _ u s e r G U I D ]     D E F A U L T   ( n e w i d ( ) ) ,  
 	 [ s p o n s o r I D ]   [ i n t ]   N U L L ,  
 	 [ n o t i f i c a t i o n F r e q u e n c y ]   [ i n t ]   N O T   N U L L ,  
 	 [ n o t i f i c a t i o n T y p e s ]   [ n v a r c h a r ] ( 1 2 8 )   N O T   N U L L ,  
 	 [ t a g s ]   [ n t e x t ]   N U L L ,  
 	 [ p r o p e r t i e s ]   [ n t e x t ]   N U L L ,  
   C O N S T R A I N T   [ P K _ U s e r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ] ,  
   C O N S T R A I N T   [ I X _ O r i g I n v i t e C o d e ]   U N I Q U E   N O N C L U S T E R E D    
 (  
 	 [ O r i g I n v i t a t i o n C o d e ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ t i t l e ]   [ n v a r c h a r ] ( 2 5 5 )   N O T   N U L L ,  
 	 [ u s e r _ i d _ o r i g i n a t o r ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ g u i d ]   [ u n i q u e i d e n t i f i e r ]   N O T   N U L L   C O N S T R A I N T   [ D F _ S t o r y _ g u i d ]     D E F A U L T   ( n e w i d ( ) ) ,  
 	 [ d e s c r i p t i o n ]   [ n t e x t ]   N U L L ,  
 	 [ s t a t e ]   [ i n t ]   N O T   N U L L   C O N S T R A I N T   [ D F _ S t o r y _ s t a t e ]     D E F A U L T   ( ( 0 ) ) ,  
 	 - - a d d e d   s t a t e   c u r s o r   f o r   g r o u p   ( p a g e )   S m a r t O r g  
 	 - - c a n n o t   m o v e   o f f   p a g e ,   u n l e s s   S t a t e C u r s o r   < =   C u r r P a g e  
 	 [ S t a t e C u r s o r ]   [ i n t ]   N O T   N U L L     D E F A U L T   ( ( 0 ) ) ,  
 	 [ t y p e S t o r y ]   [ i n t ]   N U L L ,  
 	 - - [ p r o p e r t i e s ]   [ n t e x t ]   N U L L ,  
 	 - - s t o r y C o d e A d d e d   f o r   s r c   c o d e   i n c l u d e   i n   s l i d e N a v i g a t o r   v i e w   i e   e x p o s e s   j s   f i l e s   a s   i n c l u d e s   i n   a   d i r   . . .  
 	 [ I n c l u d e C o d e D i r N a m e ]   [ n v a r c h a r ]   ( 6 4 )   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y S t a t e ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y S t a t e ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y S t a t e ] (  
 	 [ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ s t a t e N a m e ]   [ n v a r c h a r ] ( 5 0 )   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y S t a t e ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y V i e w E l e m e n t ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y V i e w E l e m e n t ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y V i e w E l e m e n t ] (  
 	 [ s t o r y _ v i e w _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ e l e m e n t _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ s e q u e n c e _ n u m b e r ]   [ i n t ]   N O T   N U L L  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S y s t e m L o g ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S y s t e m L o g ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S y s t e m L o g ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ m s g ]   [ n t e x t ]   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S y s t e m L o g ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
  
  
  
  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ e m a i l M e s s a g e S t a t e s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ e m a i l M e s s a g e S t a t e s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ e m a i l M e s s a g e S t a t e s ] (  
 	 [ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ n a m e ]   [ n v a r c h a r ] ( 5 0 )   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ e m a i l M e s s a g e S t a t e s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ F A C T _ S t o r y V i e w ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F A C T _ S t o r y V i e w ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ F A C T _ S t o r y V i e w ] (  
 	 [ s t o r y _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ r a t i n g ]   [ i n t ]   N O T   N U L L ,  
 	 [ v i e w s ]   [ i n t ]   N O T   N U L L ,  
 	 [ l a s t E d i t e d B y ]   [ i n t ]   N U L L ,  
 	 [ l a s t E d i t e d W h e n ]   [ d a t e t i m e ]   N U L L ,  
   C O N S T R A I N T   [ P K _ F A C T _ S t o r y V i e w ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ s t o r y _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ G r o u p s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ G r o u p s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ G r o u p s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ n a m e ]   [ n v a r c h a r ] ( 2 5 5 )   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ g r o u p S t a r t e d B y ]   [ i n t ]   N O T   N U L L ,  
 	 [ g u i d ]   [ u n i q u e i d e n t i f i e r ]   N O T   N U L L ,  
 	 [ d e s c r i p t i o n ]   [ n t e x t ]   N U L L ,  
   C O N S T R A I N T   [ P K _ G r o u p s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e P a g e E l e m e n t M a p ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e P a g e E l e m e n t M a p ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e P a g e E l e m e n t M a p ] (  
 	 [ p a g e _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ p a g e E l e m e n t M a p _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ s e q u e n c e ]   [ i n t ]   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e P a g e E l e m e n t M a p ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ p a g e _ i d ]   A S C ,  
 	 [ p a g e E l e m e n t M a p _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e E l e m e n t ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e E l e m e n t ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ c o d e ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ t y p e I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ v a l u e ]   [ t e x t ]   N O T   N U L L ,  
 	 [ p r e J a v a S c r i p t ]   [ n t e x t ]   N U L L ,  
 	 [ p o s t J a v a S c r i p t ]   [ n t e x t ]   N U L L ,  
 	 [ g u i d ]   [ u n i q u e i d e n t i f i e r ]   N O T   N U L L ,  
 	 [ t a g s ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ u s e r _ i d _ o r i g i n a t o r ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ p r o p e r t i e s ]   [ n t e x t ]   N U L L ,  
 	 [ l a s t M o d i f i e d ]   [ d a t e t i m e ]   N U L L ,  
 	 [ l a s t M o d i f i e d B y ]   [ i n t ]   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e E l e m e n t ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ F A C T _ S t o r y U s e r V i e w ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F A C T _ S t o r y U s e r V i e w ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ F A C T _ S t o r y U s e r V i e w ] (  
 	 [ u s e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ s t o r y _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ r a t i n g ]   [ i n t ]   N O T   N U L L ,  
 	 [ v i e w s ]   [ i n t ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y V i e w ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ u s e r _ i d ]   A S C ,  
 	 [ s t o r y _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y T x L o g ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y T x L o g ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y T x L o g ] (  
 	 [ I D ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ s t o r y _ I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ c o m m a n d ]   [ n t e x t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ u s e r _ I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ s e q ]   [ i n t ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y T x L o g ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ s t o r y _ I D ]   A S C ,  
 	 [ s e q ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
  
 / * * * * * *   O b j e c t :     I n d e x   [ I X _ S t o r y T x L o g ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . i n d e x e s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y T x L o g ] ' )   A N D   n a m e   =   N ' I X _ S t o r y T x L o g ' )  
 C R E A T E   N O N C L U S T E R E D   I N D E X   [ I X _ S t o r y T x L o g ]   O N   [ d b o ] . [ S t o r y T x L o g ]    
 (  
 	 [ s t o r y _ I D ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y C h a n g e L o g ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y C h a n g e L o g ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y C h a n g e L o g ] (  
 	 [ I D ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ s t o r y _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ s e q ]   [ i n t ]   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ c h a n g e E v e n t ]   [ n t e x t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ p r i o r i t y ]   [ i n t ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y C h a n g e L o g ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ s t o r y _ i d ]   A S C ,  
 	 [ s e q ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
  
 / * * * * * *   O b j e c t :     I n d e x   [ I X _ S t o r y C h a n g e L o g ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . i n d e x e s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y C h a n g e L o g ] ' )   A N D   n a m e   =   N ' I X _ S t o r y C h a n g e L o g ' )  
 C R E A T E   N O N C L U S T E R E D   I N D E X   [ I X _ S t o r y C h a n g e L o g ]   O N   [ d b o ] . [ S t o r y C h a n g e L o g ]    
 (  
 	 [ s t o r y _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y G r o u p E d i t o r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y G r o u p E d i t o r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y G r o u p E d i t o r s ] (  
 	 [ s t o r y _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r o u p _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y G r o u p E d i t o r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ s t o r y _ i d ]   A S C ,  
 	 [ g r o u p _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y G r o u p V i e w e r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y G r o u p V i e w e r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y G r o u p V i e w e r s ] (  
 	 [ s t o r y _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r o u p _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y G r o u p V i e w e r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ s t o r y _ i d ]   A S C ,  
 	 [ g r o u p _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y P a g e ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y P a g e ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y P a g e ] (  
 	 [ s t o r y _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ p a g e _ i d ]   [ i n t ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y P a g e ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ s t o r y _ i d ]   A S C ,  
 	 [ p a g e _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y P a g e E l e m e n t ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y P a g e E l e m e n t ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y P a g e E l e m e n t ] (  
 	 [ s t o r y _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ p a g e E l e m e n t _ i d ]   [ i n t ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y P a g e E l e m e n t ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ s t o r y _ i d ]   A S C ,  
 	 [ p a g e E l e m e n t _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y U s e r E d i t o r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y U s e r E d i t o r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y U s e r E d i t o r s ] (  
 	 [ s t o r y _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y U s e r E d i t o r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ s t o r y _ i d ]   A S C ,  
 	 [ u s e r _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t o r y U s e r V i e w e r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y U s e r V i e w e r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t o r y U s e r V i e w e r s ] (  
 	 [ s t o r y _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t o r y U s e r V i e w e r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ s t o r y _ i d ]   A S C ,  
 	 [ u s e r _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e E l e m e n t U s e r V i e w e r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t U s e r V i e w e r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e E l e m e n t U s e r V i e w e r s ] (  
 	 [ p e I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e E l e m e n t U s e r V i e w e r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ p e I D ]   A S C ,  
 	 [ u s e r _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e E l e m e n t U s e r E d i t o r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t U s e r E d i t o r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e E l e m e n t U s e r E d i t o r s ] (  
 	 [ p e I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e E l e m e n t U s e r E d i t o r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ p e I D ]   A S C ,  
 	 [ u s e r _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ U s e r s G r o u p s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ U s e r s G r o u p s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ U s e r s G r o u p s ] (  
 	 [ u s e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r o u p _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ U s e r s G r o u p s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ u s e r _ i d ]   A S C ,  
 	 [ g r o u p _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ c o d e ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ n a m e ]   [ n v a r c h a r ] ( 2 5 5 )   N O T   N U L L ,  
 	 [ s e q ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ g r i d X ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r i d Y ]   [ i n t ]   N O T   N U L L ,  
 	 [ g u i d ]   [ u n i q u e i d e n t i f i e r ]   N O T   N U L L ,  
 	 [ u s e r _ i d _ o r i g i n a t o r ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r i d Z ]   [ i n t ]   N O T   N U L L ,  
 	 [ s t y l e ]   [ i n t ]   N U L L ,  
 	 [ p r o p e r t i e s ]   [ n t e x t ]   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e U s e r V i e w e r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e U s e r V i e w e r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e U s e r V i e w e r s ] (  
 	 [ p a g e I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e U s e r V i e w e r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ p a g e I D ]   A S C ,  
 	 [ u s e r _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e U s e r E d i t o r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e U s e r E d i t o r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e U s e r E d i t o r s ] (  
 	 [ p a g e I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e U s e r E d i t o r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ p a g e I D ]   A S C ,  
 	 [ u s e r _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ S t r a t e g y T a b l e ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t r a t e g y T a b l e ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ S t r a t e g y T a b l e ] (  
 	 [ I D ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ s t J S O N 6 4 ]   [ n t e x t ]   N O T   N U L L ,  
 	 [ D a t e C r e a t e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ D a t e L a s t M o d i f i e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ o w n e r _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ n a m e 6 4 ]   [ n v a r c h a r ] ( 2 5 5 )   N O T   N U L L ,  
 	 [ G U I D ]   [ u n i q u e i d e n t i f i e r ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ S t r a t e g y T a b l e ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ I D ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
  
 / * * * * * *   O b j e c t :     I n d e x   [ I X _ S t r a t e g y T a b l e ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . i n d e x e s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t r a t e g y T a b l e ] ' )   A N D   n a m e   =   N ' I X _ S t r a t e g y T a b l e ' )  
 C R E A T E   U N I Q U E   N O N C L U S T E R E D   I N D E X   [ I X _ S t r a t e g y T a b l e ]   O N   [ d b o ] . [ S t r a t e g y T a b l e ]    
 (  
 	 [ G U I D ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ I n v i t a t i o n ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ I n v i t a t i o n ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ I n v i t a t i o n ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ v i e w U R L ]   [ n v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ i m g U R L ]   [ n v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ i n v i t a t i o n C o d e ]   [ n v a r c h a r ] ( 5 0 )   N O T   N U L L ,  
 	 [ i n v i t a t i o n T e x t ]   [ t e x t ]   N U L L ,  
 	 [ u s e r _ i d _ t o ]   [ i n t ]   N O T   N U L L ,  
 	 [ u s e r _ i d _ f r o m ]   [ i n t ]   N O T   N U L L ,  
 	 [ g u i d ]   [ u n i q u e i d e n t i f i e r ]   N O T   N U L L   C O N S T R A I N T   [ D F _ I n v i t a t i o n _ g u i d ]     D E F A U L T   ( n e w i d ( ) ) ,  
 	 [ t i t l e ]   [ n v a r c h a r ] ( 2 5 5 )   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ I n v i t e E v e n t ]   [ n v a r c h a r ] ( 5 0 )   N O T   N U L L ,  
 	 [ l a s t E r r o r ]   [ n t e x t ]   N U L L ,  
   C O N S T R A I N T   [ P K _ I n v i t a t i o n ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e G r o u p V i e w e r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e G r o u p V i e w e r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e G r o u p V i e w e r s ] (  
 	 [ p a g e I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r o u p _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e G r o u p V i e w e r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ p a g e I D ]   A S C ,  
 	 [ g r o u p _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e G r o u p E d i t o r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e G r o u p E d i t o r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e G r o u p E d i t o r s ] (  
 	 [ p a g e I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r o u p _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e G r o u p E d i t o r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ p a g e I D ]   A S C ,  
 	 [ g r o u p _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e E l e m e n t M a p ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t M a p ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e E l e m e n t M a p ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ c o d e ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ p a g e E l e m e n t _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r i d X ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r i d Y ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r i d Z ]   [ i n t ]   N U L L ,  
 	  
 	 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
 	 - -   a d d e d   f o r   s i n g l e   p a g e   e l e m e n t   s l i d e s h o w   f o r   S m a r t O r g  
 	 - -                                                                                                 1 / 2 5 / 2 0 1 0  
 	 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -    
 	 [ s e q ]   [ i n t ]   N U L L ,  
 	 [ g r o u p I D ]   [ i n t ]   N U L L ,  
 	  
 	 [ g u i d ]   [ u n i q u e i d e n t i f i e r ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	  
 	 [ u s e r _ i d _ o r i g i n a t o r ]   [ i n t ]   N U L L ,  
 	 [ l a s t M o d i f i e d ]   [ d a t e t i m e ]   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e E l e m e n t M a p ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
  
 / * * * * * *   O b j e c t :     I n d e x   [ I X _ P a g e E l e m e n t M a p ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . i n d e x e s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t M a p ] ' )   A N D   n a m e   =   N ' I X _ P a g e E l e m e n t M a p ' )  
 C R E A T E   U N I Q U E   N O N C L U S T E R E D   I N D E X   [ I X _ P a g e E l e m e n t M a p ]   O N   [ d b o ] . [ P a g e E l e m e n t M a p ]    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e E l e m e n t G r o u p E d i t o r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t G r o u p E d i t o r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e E l e m e n t G r o u p E d i t o r s ] (  
 	 [ p e I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r o u p _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e E l e m e n t G r o u p E d i t o r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ p e I D ]   A S C ,  
 	 [ g r o u p _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ P a g e E l e m e n t G r o u p V i e w e r s ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t G r o u p V i e w e r s ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ P a g e E l e m e n t G r o u p V i e w e r s ] (  
 	 [ p e I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ g r o u p _ i d ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ P a g e E l e m e n t G r o u p V i e w e r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ p e I D ]   A S C ,  
 	 [ g r o u p _ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ e m a i l M e s s a g e ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . o b j e c t s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ e m a i l M e s s a g e ] ' )   A N D   t y p e   i n   ( N ' U ' ) )  
 B E G I N  
 C R E A T E   T A B L E   [ d b o ] . [ e m a i l M e s s a g e ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ s u b j e c t ]   [ n v a r c h a r ] ( 2 5 5 )   N O T   N U L L ,  
 	 [ t o ]   [ n v a r c h a r ] ( 1 2 8 )   N O T   N U L L ,  
 	 [ f r o m ]   [ n v a r c h a r ] ( 1 2 8 )   N O T   N U L L ,  
 	 [ b o d y ]   [ n t e x t ]   N O T   N U L L ,  
 	 [ s t a t e ]   [ i n t ]   N O T   N U L L ,  
 	 [ d a t e A d d e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ u s e r A d d e d I D ]   [ i n t ]   N O T   N U L L ,  
 	 [ g u i d ]   [ u n i q u e i d e n t i f i e r ]   N O T   N U L L ,  
 	 [ r e p l y _ t o ]   [ n v a r c h a r ] ( 1 2 8 )   N O T   N U L L ,  
 	 [ l a s t M o d i f i e d ]   [ d a t e t i m e ]   N O T   N U L L ,  
 	 [ l a s t E r r o r ]   [ n t e x t ]   N U L L ,  
   C O N S T R A I N T   [ P K _ e m a i l M e s s a g e ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ i d ]   A S C  
 ) W I T H   ( I G N O R E _ D U P _ K E Y   =   O F F )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]   T E X T I M A G E _ O N   [ P R I M A R Y ]  
 E N D  
 G O  
 / * * * * * *   O b j e c t :     V i e w   [ d b o ] . [ v S t o r y P a g e L i b r a r y ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . v i e w s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ v S t o r y P a g e L i b r a r y ] ' ) )  
 E X E C   d b o . s p _ e x e c u t e s q l   @ s t a t e m e n t   =   N ' C R E A T E   V I E W   [ d b o ] . [ v S t o r y P a g e L i b r a r y ]  
 A S  
 S E L E C T           T O P   ( 1 0 0 )   P E R C E N T   d b o . P a g e E l e m e n t M a p . p a g e E l e m e n t _ i d ,   d b o . P a g e E l e m e n t M a p . g r i d X ,   d b o . P a g e E l e m e n t M a p . g r i d Y ,   d b o . P a g e E l e m e n t M a p . g r i d Z ,    
                                             d b o . P a g e . n a m e   A S   P a g e N a m e ,   d b o . P a g e . i d   A S   P a g e _ I D ,   d b o . S t o r y . i d   A S   S t o r y _ I D ,   d b o . P a g e E l e m e n t M a p . g u i d   A S   P E M _ G U I D ,    
                                             d b o . P a g e E l e m e n t M a p . d a t e A d d e d ,   d b o . P a g e . s e q   A S   P a g e S e q  
 F R O M                   d b o . P a g e   I N N E R   J O I N  
                                             d b o . P a g e P a g e E l e m e n t M a p   O N   d b o . P a g e . i d   =   d b o . P a g e P a g e E l e m e n t M a p . p a g e _ i d   I N N E R   J O I N  
                                             d b o . P a g e E l e m e n t M a p   O N   d b o . P a g e P a g e E l e m e n t M a p . p a g e E l e m e n t M a p _ i d   =   d b o . P a g e E l e m e n t M a p . i d   I N N E R   J O I N  
                                             d b o . S t o r y P a g e   O N   d b o . P a g e . i d   =   d b o . S t o r y P a g e . p a g e _ i d   I N N E R   J O I N  
                                             d b o . S t o r y   O N   d b o . S t o r y P a g e . s t o r y _ i d   =   d b o . S t o r y . i d  
 O R D E R   B Y   d b o . S t o r y P a g e . s t o r y _ i d ,   d b o . P a g e E l e m e n t M a p . p a g e E l e m e n t _ i d  
 '    
 G O  
  
 / * * * * * *   O b j e c t :     V i e w   [ d b o ] . [ v S t o r y L i b r a r y ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . v i e w s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ v S t o r y L i b r a r y ] ' ) )  
 E X E C   d b o . s p _ e x e c u t e s q l   @ s t a t e m e n t   =   N ' C R E A T E   V I E W   [ d b o ] . [ v S t o r y L i b r a r y ]  
 A S  
 S E L E C T           d b o . S t o r y . i d   A S   s t o r y _ i d ,   d b o . S t o r y . t i t l e ,   d b o . P a g e E l e m e n t . v a l u e ,   d b o . P a g e E l e m e n t . g u i d ,   d b o . P a g e E l e m e n t . i d  
 F R O M                   d b o . P a g e E l e m e n t   I N N E R   J O I N  
                                             d b o . S t o r y P a g e E l e m e n t   O N   d b o . P a g e E l e m e n t . i d   =   d b o . S t o r y P a g e E l e m e n t . p a g e E l e m e n t _ i d   I N N E R   J O I N  
                                             d b o . S t o r y   O N   d b o . S t o r y P a g e E l e m e n t . s t o r y _ i d   =   d b o . S t o r y . i d  
 '    
 G O  
  
  
 / * * * * * *   O b j e c t :     V i e w   [ d b o ] . [ v S t o r y P l a t f o r m ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . v i e w s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ v S t o r y P l a t f o r m ] ' ) )  
 E X E C   d b o . s p _ e x e c u t e s q l   @ s t a t e m e n t   =   N ' C R E A T E   V I E W   [ d b o ] . [ v S t o r y P l a t f o r m ]  
 A S  
 S E L E C T           d b o . S t o r y . t i t l e ,   d b o . F A C T _ S t o r y V i e w . r a t i n g ,   d b o . F A C T _ S t o r y V i e w . v i e w s ,   d b o . F A C T _ S t o r y V i e w . l a s t E d i t e d B y ,   d b o . F A C T _ S t o r y V i e w . l a s t E d i t e d W h e n ,    
                                             d b o . F A C T _ S t o r y V i e w . s t o r y _ i d ,   d b o . S t o r y . s t a t e  
 F R O M                   d b o . F A C T _ S t o r y V i e w   I N N E R   J O I N  
                                             d b o . S t o r y   O N   d b o . F A C T _ S t o r y V i e w . s t o r y _ i d   =   d b o . S t o r y . i d  
 '    
 G O  
  
  
 / * * * * * *   O b j e c t :     V i e w   [ d b o ] . [ v P a g e s B y S t o r y ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . v i e w s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ v P a g e s B y S t o r y ] ' ) )  
 E X E C   d b o . s p _ e x e c u t e s q l   @ s t a t e m e n t   =   N ' C R E A T E   V I E W   [ d b o ] . [ v P a g e s B y S t o r y ]  
 A S  
 S E L E C T           T O P   ( 1 0 0 )   P E R C E N T   d b o . S t o r y P a g e . s t o r y _ i d ,   d b o . S t o r y P a g e . p a g e _ i d ,   d b o . P a g e . s e q ,   d b o . P a g e . n a m e ,   d b o . P a g e . g u i d ,   d b o . P a g e . i d  
 F R O M                   d b o . P a g e   I N N E R   J O I N  
                                             d b o . S t o r y P a g e   O N   d b o . P a g e . i d   =   d b o . S t o r y P a g e . p a g e _ i d  
 O R D E R   B Y   d b o . P a g e . s e q  
 '    
 G O  
 / * * * * * *   O b j e c t :     V i e w   [ d b o ] . [ v E m a i l M e s s a g e ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . v i e w s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ v E m a i l M e s s a g e ] ' ) )  
 E X E C   d b o . s p _ e x e c u t e s q l   @ s t a t e m e n t   =   N ' C R E A T E   V I E W   [ d b o ] . [ v E m a i l M e s s a g e ]  
 A S  
 S E L E C T           d b o . e m a i l M e s s a g e . i d ,   d b o . e m a i l M e s s a g e . s u b j e c t ,   d b o . e m a i l M e s s a g e . [ t o ]   A S   ' ' t o A d d r e s s ' ' ,   d b o . e m a i l M e s s a g e . [ f r o m ]   A S   ' ' f r o m A d d r e s s ' ' ,    
                                             d b o . e m a i l M e s s a g e . b o d y ,   d b o . e m a i l M e s s a g e S t a t e s . n a m e   A S   ' ' s t a t e H R ' ' ,   d b o . e m a i l M e s s a g e . s t a t e ,   d b o . e m a i l M e s s a g e . d a t e A d d e d ,    
                                             d b o . e m a i l M e s s a g e . u s e r A d d e d I D ,   d b o . e m a i l M e s s a g e . g u i d ,   d b o . e m a i l M e s s a g e . r e p l y _ t o ,   d b o . e m a i l M e s s a g e . l a s t M o d i f i e d ,    
                                             d b o . U s e r s . f i r s t N a m e   A S   ' ' t o F i r s t N a m e ' ' ,   d b o . U s e r s . l a s t N a m e   A S   ' ' t o L a s t N a m e ' ' ,   d b o . U s e r s . e m a i l ,   d b o . U s e r s . O r i g I n v i t a t i o n C o d e  
 F R O M                   d b o . e m a i l M e s s a g e   I N N E R   J O I N  
                                             d b o . e m a i l M e s s a g e S t a t e s   O N   d b o . e m a i l M e s s a g e . s t a t e   =   d b o . e m a i l M e s s a g e S t a t e s . i d   I N N E R   J O I N  
                                             d b o . U s e r s   O N   d b o . e m a i l M e s s a g e . u s e r A d d e d I D   =   d b o . U s e r s . i d  
 '    
 G O  
 / * * * * * *   O b j e c t :     V i e w   [ d b o ] . [ v P a g e E l e m B y U s e r ]         S c r i p t   D a t e :   0 3 / 0 5 / 2 0 0 8   0 6 : 3 1 : 5 7   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . v i e w s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ v P a g e E l e m B y U s e r ] ' ) )  
 E X E C   d b o . s p _ e x e c u t e s q l   @ s t a t e m e n t   =   N ' C R E A T E   V I E W   [ d b o ] . [ v P a g e E l e m B y U s e r ]  
 A S  
 S E L E C T           T O P   ( 1 0 0 )   P E R C E N T   d b o . P a g e E l e m e n t . i d ,   d b o . P a g e E l e m e n t . c o d e ,   d b o . P a g e E l e m e n t . t y p e I D ,   d b o . P a g e E l e m e n t . v a l u e ,   d b o . P a g e E l e m e n t . g u i d ,    
                                             d b o . P a g e E l e m e n t . t a g s ,   d b o . P a g e E l e m e n t . u s e r _ i d _ o r i g i n a t o r ,   d b o . P a g e E l e m e n t . d a t e A d d e d ,   d b o . P a g e E l e m e n t . p r o p e r t i e s ,    
                                             d b o . P a g e E l e m e n t . l a s t M o d i f i e d ,   d b o . P a g e E l e m e n t . l a s t M o d i f i e d B y ,   d b o . U s e r s . u s e r n a m e ,   d b o . U s e r s . i d   A S   U s e r O r i g i n a t o r  
 F R O M                   d b o . P a g e E l e m e n t   I N N E R   J O I N  
                                             d b o . U s e r s   O N   d b o . P a g e E l e m e n t . u s e r _ i d _ o r i g i n a t o r   =   d b o . U s e r s . i d  
 '    
 G O  
  
  
 U S E   [ A g e n t S t o r y E v o l u t i o n ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S p o s o r S e l f ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ U s e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ U s e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S p o s o r S e l f ]   F O R E I G N   K E Y ( [ s p o n s o r I D ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e P a g e E l e m e n t M a p _ P a g e ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e P a g e E l e m e n t M a p ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e P a g e E l e m e n t M a p ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e P a g e E l e m e n t M a p _ P a g e ]   F O R E I G N   K E Y ( [ p a g e _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ P a g e P a g e E l e m e n t M a p ]   C H E C K   C O N S T R A I N T   [ F K _ P a g e P a g e E l e m e n t M a p _ P a g e ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e P a g e E l e m e n t M a p _ P a g e E l e m e n t M a p ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e P a g e E l e m e n t M a p ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e P a g e E l e m e n t M a p ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e P a g e E l e m e n t M a p _ P a g e E l e m e n t M a p ]   F O R E I G N   K E Y ( [ p a g e E l e m e n t M a p _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e E l e m e n t M a p ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ P a g e P a g e E l e m e n t M a p ]   C H E C K   C O N S T R A I N T   [ F K _ P a g e P a g e E l e m e n t M a p _ P a g e E l e m e n t M a p ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t _ P a g e E l e m e n t T y p e ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t _ P a g e E l e m e n t T y p e ]   F O R E I G N   K E Y ( [ t y p e I D ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e E l e m e n t T y p e ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t ]   C H E C K   C O N S T R A I N T   [ F K _ P a g e E l e m e n t _ P a g e E l e m e n t T y p e ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d _ o r i g i n a t o r ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t ]   C H E C K   C O N S T R A I N T   [ F K _ P a g e E l e m e n t _ U s e r s ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y V i e w _ S t o r y ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F A C T _ S t o r y U s e r V i e w ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ F A C T _ S t o r y U s e r V i e w ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y V i e w _ S t o r y ]   F O R E I G N   K E Y ( [ s t o r y _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y V i e w _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F A C T _ S t o r y U s e r V i e w ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ F A C T _ S t o r y U s e r V i e w ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y V i e w _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y T x L o g _ S t o r y ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y T x L o g ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y T x L o g ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y T x L o g _ S t o r y ]   F O R E I G N   K E Y ( [ s t o r y _ I D ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y T x L o g _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y T x L o g ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y T x L o g ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y T x L o g _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ I D ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y C h a n g e L o g _ S t o r y ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y C h a n g e L o g ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y C h a n g e L o g ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y C h a n g e L o g _ S t o r y ]   F O R E I G N   K E Y ( [ s t o r y _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y C h a n g e L o g _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y C h a n g e L o g ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y C h a n g e L o g ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y C h a n g e L o g _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y G r o u p E d i t o r s _ G r o u p s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y G r o u p E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y G r o u p E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y G r o u p E d i t o r s _ G r o u p s ]   F O R E I G N   K E Y ( [ g r o u p _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ G r o u p s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y G r o u p E d i t o r s _ S t o r y ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y G r o u p E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y G r o u p E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y G r o u p E d i t o r s _ S t o r y ]   F O R E I G N   K E Y ( [ s t o r y _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y G r o u p V i e w e r s _ G r o u p s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y G r o u p V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y G r o u p V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y G r o u p V i e w e r s _ G r o u p s ]   F O R E I G N   K E Y ( [ g r o u p _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ G r o u p s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y G r o u p V i e w e r s _ S t o r y ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y G r o u p V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y G r o u p V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y G r o u p V i e w e r s _ S t o r y ]   F O R E I G N   K E Y ( [ s t o r y _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y P a g e _ P a g e ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y P a g e ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y P a g e ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y P a g e _ P a g e ]   F O R E I G N   K E Y ( [ p a g e _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y P a g e ]   C H E C K   C O N S T R A I N T   [ F K _ S t o r y P a g e _ P a g e ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y P a g e _ S t o r y ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y P a g e ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y P a g e ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y P a g e _ S t o r y ]   F O R E I G N   K E Y ( [ s t o r y _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y P a g e ]   C H E C K   C O N S T R A I N T   [ F K _ S t o r y P a g e _ S t o r y ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y P a g e E l e m e n t _ P a g e E l e m e n t ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y P a g e E l e m e n t ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y P a g e E l e m e n t ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y P a g e E l e m e n t _ P a g e E l e m e n t ]   F O R E I G N   K E Y ( [ p a g e E l e m e n t _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e E l e m e n t ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y P a g e E l e m e n t ]   C H E C K   C O N S T R A I N T   [ F K _ S t o r y P a g e E l e m e n t _ P a g e E l e m e n t ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y P a g e E l e m e n t _ S t o r y ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y P a g e E l e m e n t ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y P a g e E l e m e n t ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y P a g e E l e m e n t _ S t o r y ]   F O R E I G N   K E Y ( [ s t o r y _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y P a g e E l e m e n t ]   C H E C K   C O N S T R A I N T   [ F K _ S t o r y P a g e E l e m e n t _ S t o r y ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y U s e r E d i t o r s _ S t o r y ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y U s e r E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y U s e r E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y U s e r E d i t o r s _ S t o r y ]   F O R E I G N   K E Y ( [ s t o r y _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y U s e r E d i t o r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y U s e r E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y U s e r E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y U s e r E d i t o r s _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y U s e r V i e w e r s _ S t o r y ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y U s e r V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y U s e r V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y U s e r V i e w e r s _ S t o r y ]   F O R E I G N   K E Y ( [ s t o r y _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y U s e r V i e w e r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y U s e r V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y U s e r V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y U s e r V i e w e r s _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t U s e r V i e w e r s _ P a g e E l e m e n t ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t U s e r V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t U s e r V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t U s e r V i e w e r s _ P a g e E l e m e n t ]   F O R E I G N   K E Y ( [ p e I D ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e E l e m e n t ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t U s e r V i e w e r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t U s e r V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t U s e r V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t U s e r V i e w e r s _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t U s e r E d i t o r s _ P a g e E l e m e n t ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t U s e r E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t U s e r E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t U s e r E d i t o r s _ P a g e E l e m e n t ]   F O R E I G N   K E Y ( [ p e I D ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e E l e m e n t ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t U s e r E d i t o r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t U s e r E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t U s e r E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t U s e r E d i t o r s _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ U s e r s G r o u p s _ G r o u p s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ U s e r s G r o u p s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ U s e r s G r o u p s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ U s e r s G r o u p s _ G r o u p s ]   F O R E I G N   K E Y ( [ g r o u p _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ G r o u p s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ U s e r s G r o u p s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ U s e r s G r o u p s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ U s e r s G r o u p s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ U s e r s G r o u p s _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y _ S t o r y S t a t e ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y _ S t o r y S t a t e ]   F O R E I G N   K E Y ( [ s t a t e ] )  
 R E F E R E N C E S   [ d b o ] . [ S t o r y S t a t e ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y ]   C H E C K   C O N S T R A I N T   [ F K _ S t o r y _ S t o r y S t a t e ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t o r y _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t o r y ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ S t o r y _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d _ o r i g i n a t o r ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ S t o r y ]   C H E C K   C O N S T R A I N T   [ F K _ S t o r y _ U s e r s ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d _ o r i g i n a t o r ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ P a g e ]   C H E C K   C O N S T R A I N T   [ F K _ P a g e _ U s e r s ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e U s e r V i e w e r s _ P a g e ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e U s e r V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e U s e r V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e U s e r V i e w e r s _ P a g e ]   F O R E I G N   K E Y ( [ p a g e I D ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e U s e r V i e w e r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e U s e r V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e U s e r V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e U s e r V i e w e r s _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e U s e r E d i t o r s _ P a g e ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e U s e r E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e U s e r E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e U s e r E d i t o r s _ P a g e ]   F O R E I G N   K E Y ( [ p a g e I D ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e U s e r E d i t o r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e U s e r E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e U s e r E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e U s e r E d i t o r s _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ S t r a t e g y T a b l e _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ S t r a t e g y T a b l e ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ S t r a t e g y T a b l e ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ S t r a t e g y T a b l e _ U s e r s ]   F O R E I G N   K E Y ( [ o w n e r _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ I n v i t a t i o n _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ I n v i t a t i o n ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ I n v i t a t i o n ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ I n v i t a t i o n _ U s e r s ]   F O R E I G N   K E Y ( [ u s e r _ i d _ t o ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ I n v i t a t i o n _ U s e r s 1 ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ I n v i t a t i o n ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ I n v i t a t i o n ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ I n v i t a t i o n _ U s e r s 1 ]   F O R E I G N   K E Y ( [ u s e r _ i d _ f r o m ] )  
 R E F E R E N C E S   [ d b o ] . [ U s e r s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e G r o u p V i e w e r s _ P a g e ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e G r o u p V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e G r o u p V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e G r o u p V i e w e r s _ P a g e ]   F O R E I G N   K E Y ( [ p a g e I D ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e G r o u p V i e w e r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e G r o u p V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e G r o u p V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e G r o u p V i e w e r s _ U s e r s ]   F O R E I G N   K E Y ( [ g r o u p _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ G r o u p s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e G r o u p E d i t o r s _ P a g e ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e G r o u p E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e G r o u p E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e G r o u p E d i t o r s _ P a g e ]   F O R E I G N   K E Y ( [ p a g e I D ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e G r o u p E d i t o r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e G r o u p E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e G r o u p E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e G r o u p E d i t o r s _ U s e r s ]   F O R E I G N   K E Y ( [ g r o u p _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ G r o u p s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t M a p _ P a g e E l e m e n t ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t M a p ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t M a p ]     W I T H   N O C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t M a p _ P a g e E l e m e n t ]   F O R E I G N   K E Y ( [ p a g e E l e m e n t _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e E l e m e n t ]   ( [ i d ] )  
 G O  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t M a p ]   C H E C K   C O N S T R A I N T   [ F K _ P a g e E l e m e n t M a p _ P a g e E l e m e n t ]  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t G r o u p E d i t o r s _ P a g e E l e m e n t ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t G r o u p E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t G r o u p E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t G r o u p E d i t o r s _ P a g e E l e m e n t ]   F O R E I G N   K E Y ( [ p e I D ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e E l e m e n t ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t G r o u p E d i t o r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t G r o u p E d i t o r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t G r o u p E d i t o r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t G r o u p E d i t o r s _ U s e r s ]   F O R E I G N   K E Y ( [ g r o u p _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ G r o u p s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t G r o u p V i e w e r s _ P a g e E l e m e n t ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t G r o u p V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t G r o u p V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t G r o u p V i e w e r s _ P a g e E l e m e n t ]   F O R E I G N   K E Y ( [ p e I D ] )  
 R E F E R E N C E S   [ d b o ] . [ P a g e E l e m e n t ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ P a g e E l e m e n t G r o u p V i e w e r s _ U s e r s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ P a g e E l e m e n t G r o u p V i e w e r s ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ P a g e E l e m e n t G r o u p V i e w e r s ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ P a g e E l e m e n t G r o u p V i e w e r s _ U s e r s ]   F O R E I G N   K E Y ( [ g r o u p _ i d ] )  
 R E F E R E N C E S   [ d b o ] . [ G r o u p s ]   ( [ i d ] )  
 G O  
 I F   N O T   E X I S T S   ( S E L E C T   *   F R O M   s y s . f o r e i g n _ k e y s   W H E R E   o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ F K _ e m a i l M e s s a g e _ e m a i l M e s s a g e S t a t e s ] ' )   A N D   p a r e n t _ o b j e c t _ i d   =   O B J E C T _ I D ( N ' [ d b o ] . [ e m a i l M e s s a g e ] ' ) )  
 A L T E R   T A B L E   [ d b o ] . [ e m a i l M e s s a g e ]     W I T H   C H E C K   A D D     C O N S T R A I N T   [ F K _ e m a i l M e s s a g e _ e m a i l M e s s a g e S t a t e s ]   F O R E I G N   K E Y ( [ s t a t e ] )  
 R E F E R E N C E S   [ d b o ] . [ e m a i l M e s s a g e S t a t e s ]   ( [ i d ] )  
 