/****** Object:  StoredProcedure [dbo].[SP_GetCustomerFixedIncomeAccount]    Script Date: 06/24/2009 12:00:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerFixedIncomeAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerFixedIncomeAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerFixedIncomeAccount]    Script Date: 06/24/2009 12:00:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerFixedIncomeAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerFixedIncomeAccount]

@CFIA_AccountId INT

AS

SELECT * FROM dbo.CustomerFixedIncomeAccount WHERE CFIA_AccountId=@CFIA_AccountId' 
END
GO
