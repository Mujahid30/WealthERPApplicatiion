/****** Object:  StoredProcedure [dbo].[SP_GetAssetGroups]    Script Date: 06/24/2009 11:58:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetGroups]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAssetGroups]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAssetGroups]    Script Date: 06/24/2009 11:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAssetGroups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAssetGroups]

as

select * from ProductAssetGroup' 
END
GO
