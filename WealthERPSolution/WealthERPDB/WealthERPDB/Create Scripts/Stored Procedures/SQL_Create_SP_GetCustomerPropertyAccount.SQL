/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPropertyAccount]    Script Date: 06/24/2009 12:01:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPropertyAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerPropertyAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPropertyAccount]    Script Date: 06/24/2009 12:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPropertyAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetCustomerPropertyAccount]
@CPA_AccountId INT

AS

BEGIN
	
	SELECT * FROM
	dbo.CustomerPropertyAccount
	WHERE CPA_AccountId = @CPA_AccountId
	
END' 
END
GO
