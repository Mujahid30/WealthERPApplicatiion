/****** Object:  StoredProcedure [dbo].[SP_GetCustomerDefaultPortfolio]    Script Date: 06/24/2009 11:59:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerDefaultPortfolio]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerDefaultPortfolio]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerDefaultPortfolio]    Script Date: 06/24/2009 11:59:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerDefaultPortfolio]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerDefaultPortfolio]
@C_CustomerId INT

AS

SELECT * FROM dbo.CustomerPortfolio
WHERE
C_CustomerId=@C_CustomerId and CP_IsMainPortfolio=1

' 
END
GO
