/****** Object:  StoredProcedure [dbo].[SP_FindRM]    Script Date: 06/24/2009 11:15:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FindRM]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_FindRM]
GO
/****** Object:  StoredProcedure [dbo].[SP_FindRM]    Script Date: 06/24/2009 11:15:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FindRM]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_FindRM]
@AR_FirstName VARCHAR(50),
@A_AdviserId int

AS

SELECT  AR_RMId 
,AR_FirstName
--+ AR_LastName+AR_MiddleName AS NAME  
FROM dbo.AdviserRM 
WHERE 
	AR_FirstName LIKE @AR_FirstName + ''%'' 
	--+ AR_LastName+AR_MiddleName 
 AND A_AdviserId=@A_AdviserId
 
 --EXEC dbo.SP_FindRM ''r'' ,1004
	--@AR_FirstName = '''', -- VARCHAR(50)
	--@A_AdviserId = 0 -- int

 





' 
END
GO
