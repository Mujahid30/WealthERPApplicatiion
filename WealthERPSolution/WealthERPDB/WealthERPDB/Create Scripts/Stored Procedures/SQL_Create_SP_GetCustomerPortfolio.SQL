/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPortfolio]    Script Date: 06/24/2009 12:00:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPortfolio]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerPortfolio]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPortfolio]    Script Date: 06/24/2009 12:00:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPortfolio]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetCustomerPortfolio
@C_CustomerId INT

AS

SELECT * FROM dbo.CustomerPortfolio
WHERE
C_CustomerId=@C_CustomerId
' 
END
GO
