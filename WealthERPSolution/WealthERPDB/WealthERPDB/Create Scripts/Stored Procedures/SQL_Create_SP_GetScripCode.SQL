/****** Object:  StoredProcedure [dbo].[SP_GetScripCode]    Script Date: 06/24/2009 12:04:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetScripCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetScripCode]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetScripCode]    Script Date: 06/24/2009 12:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetScripCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetScripCode]
@PEM_CompanyName VARCHAR(MAX)
AS
SELECT * FROM dbo.ProductEquityMaster WHERE PEM_CompanyName=@PEM_CompanyName' 
END
GO
