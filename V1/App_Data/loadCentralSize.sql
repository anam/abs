Declare @LastStyle nvarchar(256)
Declare @Style nvarchar(256)
declare @ID int
DECLARE product_cursor CURSOR FOR 
    SELECT ID,Size
    FROM Table_30

    OPEN product_cursor
    FETCH NEXT FROM product_cursor INTO @ID,@Style

    
    WHILE @@FETCH_STATUS = 0
    BEGIN
	
        update Table_30 set Size =(Select cast(Pos_SizeID as nvarchar(50)) from Pos_Size where SizeName=@Style)
        where ID=@ID
        
        FETCH NEXT FROM product_cursor INTO @ID,@Style
        END

    CLOSE product_cursor
    DEALLOCATE product_cursor
    
    Select * from Table_30