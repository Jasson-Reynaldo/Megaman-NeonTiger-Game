Imports System.IO
'This Program is Made by FJR
' - Francis Dwiputra Abdikesuma (001201900013)
' - Jasson Reynaldo (001201900063)
' - Renaldo (001201900074)
Public Class MainProgram
    Dim bmp As Bitmap
    Dim Bg, Img As CImage
    Dim SpriteMap As CImage
    Dim SpriteMask As CImage
    Dim TigerIntro, TigerStand, TigerJumpStart, TigerJumpUpForward, TigerJumpDown, TigerLanding, TigerClingToWall, TigerRaySplashWall, TigerDiveAtt, TigerFallDown, TigerRaySplashGround, TigerShotBlock, TigerRushAttGround, TigerRushAttAir, TigerMeleeCombo, TigerJumpDownR, TigerJumpDownM, TigerLandingR, TigerDeath As CArrFrame
    Dim MegamanIntro, MegamanWalk, MegamanJump, MegamanJumpDown, MegamanShoot, MegamanWalkShoot, MegamanJumpShoot, MegamanStand, MegamanElectrocute, MegamanDeath As CArrFrame
    Dim TigerProjCreate As CArrFrame
    Dim MegamanProjCreate As CArrFrame
    Dim ListChar As New List(Of CCharacter)
    Dim List_MP = New List(Of CCharMegaProjectile)
    Dim List_TP = New List(Of CCharTigerProjectile)
    Dim NT As CCharNeonTiger
    Dim MM As CCharMegaman
    Dim MP As CCharMegaProjectile
    Dim TP As CCharTigerProjectile
    Dim Respawn As Integer

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Bg = New CImage
        Bg.OpenImage("D:\NeonTigerVsMegaman(FJR-IT4)\Bg.bmp") 'for background

        Bg.CopyImg(Img)

        SpriteMap = New CImage
        SpriteMap.OpenImage("D:\NeonTigerVsMegaman(FJR-IT4)\Sprite.bmp") 'for spritesheet

        SpriteMap.CreateMask(SpriteMask)

        TigerIntro = New CArrFrame
        TigerIntro.Insert(186, 145, 116, 52, 254, 197, 3)
        TigerIntro.Insert(380, 145, 311, 51, 450, 197, 3)
        TigerIntro.Insert(579, 145, 523, 85, 650, 195, 5)
        TigerIntro.Insert(781, 144, 706, 114, 839, 194, 5)
        TigerIntro.Insert(966, 144, 904, 96, 1027, 195, 5)
        TigerIntro.Insert(1138, 144, 1082, 106, 1200, 194, 3)
        TigerIntro.Insert(1301, 144, 1262, 102, 1384, 195, 3)
        TigerIntro.Insert(1484, 144, 1443, 85, 1570, 196, 5)
        TigerIntro.Insert(1673, 144, 1625, 80, 1746, 198, 5)
        TigerIntro.Insert(1923, 144, 1824, 112, 1985, 193, 10)

        TigerStand = New CArrFrame
        TigerStand.Insert(966, 144, 905, 93, 1027, 195, 1, 930, 120, 996, 190)

        TigerJumpStart = New CArrFrame
        TigerJumpStart.Insert(1365, 321, 1300, 284, 1430, 374, 4, 1342, 308, 1404, 368)

        TigerJumpUpForward = New CArrFrame
        TigerJumpUpForward.Insert(148, 822, 96, 782, 200, 868, 4, 104, 792, 170, 858)
        TigerJumpUpForward.Insert(337, 822, 276, 782, 398, 867, 4, 292, 792, 358, 858)

        TigerJumpDown = New CArrFrame
        TigerJumpDown.Insert(540, 834, 480, 771, 600, 882, 4, 508, 786, 560, 866)
        TigerJumpDown.Insert(737, 835, 669, 770, 805, 882, 4, 706, 789, 760, 866)

        TigerLanding = New CArrFrame
        TigerLanding.Insert(781, 144, 706, 114, 839, 194, 3, 734, 134, 802, 190)

        TigerClingToWall = New CArrFrame
        TigerClingToWall.Insert(114, 660, 76, 584, 168, 714, 4, 105, 627, 161, 705)
        TigerClingToWall.Insert(298, 660, 241, 584, 351, 714, 4, 287, 627, 343, 705)

        TigerRaySplashWall = New CArrFrame
        TigerRaySplashWall.Insert(476, 660, 421, 584, 527, 714, 1, 464, 627, 523, 705)
        TigerRaySplashWall.Insert(643, 660, 587, 584, 696, 714, 1, 631, 627, 688, 705)
        TigerRaySplashWall.Insert(820, 660, 767, 584, 873, 714, 1, 808, 627, 866, 705)
        TigerRaySplashWall.Insert(993, 660, 936, 584, 1050, 714, 1, 983, 627, 1039, 705)
        TigerRaySplashWall.Insert(1180, 660, 1123, 584, 1237, 714, 1, 1168, 627, 1227, 705)
        TigerRaySplashWall.Insert(1359, 660, 1299, 584, 1419, 714, 1, 1349, 627, 1405, 705)
        TigerRaySplashWall.Insert(1544, 660, 1468, 584, 1600, 714, 1, 1534, 627, 1590, 705)
        TigerRaySplashWall.Insert(1731, 660, 1654, 584, 1788, 714, 4, 1721, 627, 1777, 705)

        TigerDiveAtt = New CArrFrame
        TigerDiveAtt.Insert(161, 145, 116, 52, 254, 197, 3, 150, 105, 207, 175)
        TigerDiveAtt.Insert(355, 145, 311, 51, 450, 197, 3, 345, 108, 400, 125)
        TigerDiveAtt.Insert(141, 321, 75, 249, 207, 373, 3, 128, 300, 184, 368)
        TigerDiveAtt.Insert(432, 321, 334, 250, 510, 374, 4, 340, 305, 452, 368)
        TigerDiveAtt.Insert(667, 321, 586, 294, 748, 388, 4, 626, 308, 685, 368)
        TigerDiveAtt.Insert(900, 321, 820, 295, 980, 372, 4, 856, 305, 923, 368)
        TigerDiveAtt.Insert(1148, 321, 1076, 290, 1206, 372, 4, 1104, 308, 1170, 368)
        TigerDiveAtt.Insert(1365, 321, 1306, 284, 1440, 374, 4, 1342, 308, 1404, 368)

        TigerFallDown = New CArrFrame
        TigerFallDown.Insert(1690, 1612, 1620, 1558, 1760, 1658, 3, 1660, 1565, 1710, 1652)
        TigerFallDown.Insert(0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0)

        TigerRaySplashGround = New CArrFrame
        TigerRaySplashGround.Insert(163, 463, 102, 430, 224, 515, 1, 121, 452, 199, 510)
        TigerRaySplashGround.Insert(403, 463, 325, 440, 463, 513, 1, 364, 462, 452, 510)
        TigerRaySplashGround.Insert(651, 463, 571, 440, 709, 515, 1, 612, 463, 673, 510)
        TigerRaySplashGround.Insert(886, 463, 805, 440, 948, 515, 1, 846, 463, 908, 510)
        TigerRaySplashGround.Insert(1117, 463, 1036, 440, 1176, 515, 1, 1078, 463, 1137, 510)
        TigerRaySplashGround.Insert(1358, 463, 1273, 440, 1420, 515, 1, 1320, 463, 1378, 510)
        TigerRaySplashGround.Insert(1610, 463, 1523, 440, 1672, 515, 1, 1571, 463, 1631, 510)
        TigerRaySplashGround.Insert(1880, 463, 1777, 440, 1938, 515, 1, 1841, 463, 1631, 510)
        TigerRaySplashGround.Insert(2116, 463, 2009, 440, 2175, 515, 4, 2078, 463, 2138, 510)

        TigerRushAttGround = New CArrFrame
        TigerRushAttGround.Insert(164, 994, 100, 945, 220, 1046, 3, 129, 972, 198, 1039)
        TigerRushAttGround.Insert(365, 994, 302, 945, 422, 1046, 3, 330, 973, 397, 1039)
        TigerRushAttGround.Insert(520, 994, 484, 950, 600, 1046, 3, 507, 962, 571, 1037)
        TigerRushAttGround.Insert(714, 994, 628, 937, 792, 1046, 3, 700, 965, 766, 1038)
        TigerRushAttGround.Insert(911, 994, 874, 950, 990, 1046, 3, 897, 967, 964, 1038)
        TigerRushAttGround.Insert(1110, 994, 1072, 938, 1190, 1046, 3, 1093, 966, 1164, 1039)
        TigerRushAttGround.Insert(1312, 994, 1274, 950, 1392, 1046, 3, 1298, 968, 1363, 1038)
        TigerRushAttGround.Insert(1501, 994, 1462, 938, 1580, 1046, 3, 1486, 966, 1553, 1040)
        TigerRushAttGround.Insert(157, 1156, 95, 1108, 213, 1209, 3, 123, 1137, 183, 1200)
        TigerRushAttGround.Insert(373, 1156, 304, 1126, 430, 1209, 3, 327, 1148, 400, 1201)
        TigerRushAttGround.Insert(572, 1156, 516, 1121, 638, 1209, 3, 543, 1145, 613, 1193)
        TigerRushAttGround.Insert(162, 1345, 92, 1296, 232, 1394, 3, 124, 1309, 192, 1386)
        TigerRushAttGround.Insert(407, 1345, 337, 1296, 477, 1394, 3, 368, 1311, 434, 1386)

        TigerShotBlock = New CArrFrame
        TigerShotBlock.Insert(1138, 144, 1082, 106, 1200, 194, 3, 1114, 124, 1182, 190)
        TigerShotBlock.Insert(1301, 144, 1262, 102, 1384, 195, 5, 1287, 106, 1356, 190)
        TigerShotBlock.Insert(1484, 144, 1443, 85, 1570, 196, 5, 1470, 92, 1538, 190)

        TigerRushAttAir = New CArrFrame
        TigerRushAttAir.Insert(572, 1156, 516, 1121, 638, 1209, 3, 543, 1145, 613, 1193)
        TigerRushAttAir.Insert(788, 1161, 740, 1117, 854, 1204, 3, 745, 1130, 811, 1189)
        TigerRushAttAir.Insert(982, 1161, 932, 1117, 1040, 1204, 3, 938, 1125, 1014, 1196)

        TigerMeleeCombo = New CArrFrame
        TigerMeleeCombo.Insert(1918, 1006, 1880, 934, 1984, 1058, 2, 1906, 971, 1958, 1050)
        TigerMeleeCombo.Insert(1225, 1156, 1128, 1091, 1299, 1203, 2, 1133, 1120, 1256, 1201)
        TigerMeleeCombo.Insert(1459, 1156, 1382, 1135, 1535, 1221, 2, 1407, 1137, 1500, 1215)
        TigerMeleeCombo.Insert(1642, 1156, 1596, 1132, 1718, 1221, 3, 1600, 1138, 1684, 1204)
        TigerMeleeCombo.Insert(372, 1156, 303, 1127, 428, 1209, 3, 327, 1148, 400, 1200)
        TigerMeleeCombo.Insert(157, 1156, 95, 1108, 213, 1209, 3, 123, 1137, 183, 1200)
        TigerMeleeCombo.Insert(572, 1156, 516, 1121, 637, 1204, 3, 543, 1145, 613, 1193)
        TigerMeleeCombo.Insert(1723, 1011, 1694, 934, 1788, 1058, 3, 1700, 973, 1746, 1040)
        TigerMeleeCombo.Insert(689, 1346, 603, 1298, 769, 1393, 8, 608, 1305, 735, 1379)
        TigerMeleeCombo.Insert(925, 1346, 842, 1257, 1005, 1396, 5, 868, 1265, 966, 1393)

        TigerJumpDownR = New CArrFrame
        TigerJumpDownR.Insert(1645, 1345, 1578, 1257, 1710, 1394, 3, 1604, 1308, 1670, 1388)
        TigerJumpDownR.Insert(1871, 1345, 1803, 1255, 1937, 1395, 3, 1830, 1308, 1895, 1388)

        TigerJumpDownM = New CArrFrame
        TigerJumpDownM.Insert(1173, 1345, 1136, 1256, 1254, 1397, 10, 1142, 1300, 1211, 1388)
        TigerJumpDownM.Insert(1389, 1345, 1353, 1259, 1469, 1395, 30, 1357, 1300, 1426, 1388)

        TigerLandingR = New CArrFrame
        TigerLandingR.Insert(2069, 1345, 2015, 1289, 2135, 1397, 4, 2030, 1303, 2107, 1387)
        TigerLandingR.Insert(157, 1156, 95, 1110, 215, 1208, 4, 123, 1137, 183, 1200)
        TigerLandingR.Insert(373, 1156, 304, 1126, 426, 1208, 4, 327, 1148, 400, 1201)
        TigerLandingR.Insert(966, 144, 904, 96, 1027, 195, 3, 930, 120, 996, 190)

        TigerDeath = New CArrFrame
        TigerDeath.Insert(1690, 1612, 1620, 1558, 1760, 1658, 10, 1660, 1565, 1710, 1652)

        NT = New CCharNeonTiger
        ReDim NT.ArrSprites(18)
        NT.ArrSprites(0) = TigerIntro
        NT.ArrSprites(1) = TigerStand
        NT.ArrSprites(2) = TigerJumpStart
        NT.ArrSprites(3) = TigerJumpUpForward
        NT.ArrSprites(4) = TigerJumpDown
        NT.ArrSprites(5) = TigerLanding
        NT.ArrSprites(6) = TigerClingToWall
        NT.ArrSprites(7) = TigerRaySplashWall
        NT.ArrSprites(8) = TigerDiveAtt
        NT.ArrSprites(9) = TigerFallDown
        NT.ArrSprites(10) = TigerRaySplashGround
        NT.ArrSprites(11) = TigerShotBlock
        NT.ArrSprites(12) = TigerRushAttGround
        NT.ArrSprites(13) = TigerRushAttAir
        NT.ArrSprites(14) = TigerMeleeCombo
        NT.ArrSprites(15) = TigerJumpDownR
        NT.ArrSprites(16) = TigerJumpDownM
        NT.ArrSprites(17) = TigerLandingR
        NT.ArrSprites(18) = TigerDeath

        NT.PosX = 420
        NT.PosY = 100
        NT.Vx = -5
        NT.Vy = 7
        NT.State(StateNeonTiger.Intro, 0)
        NT.FDir = FaceDir.Left

        ListChar.Add(NT)

        MegamanIntro = New CArrFrame
        MegamanIntro.Insert(2506, 125, 2491, 78, 2521, 171, 3, 2500, 83, 2513, 169)
        MegamanIntro.Insert(2607, 125, 2585, 95, 2630, 171, 3, 2589, 118, 2627, 168)
        MegamanIntro.Insert(2719, 125, 2689, 108, 2750, 171, 3, 2704, 109, 2739, 168)
        MegamanIntro.Insert(2832, 125, 2803, 108, 2863, 171, 3, 2818, 110, 2852, 168)

        MegamanStand = New CArrFrame
        MegamanStand.Insert(2940, 125, 2913, 100, 2972, 171, 1, 2934, 107, 2959, 168)

        MegamanWalk = New CArrFrame
        MegamanWalk.Insert(2466, 343, 2439, 321, 2499, 389, 2, 2460, 325, 2488, 382)
        MegamanWalk.Insert(2570, 343, 2555, 321, 2597, 389, 2, 2565, 323, 2591, 382)
        MegamanWalk.Insert(2667, 343, 2644, 321, 2690, 389, 2, 2660, 323, 2684, 382)
        MegamanWalk.Insert(2780, 343, 2748, 321, 2810, 389, 2, 2775, 325, 2798, 383)
        MegamanWalk.Insert(2890, 343, 2867, 321, 2913, 389, 2, 2883, 325, 2908, 383)
        MegamanWalk.Insert(2995, 343, 2970, 321, 3020, 389, 2, 2988, 323, 3014, 383)
        MegamanWalk.Insert(3089, 343, 3055, 321, 3123, 389, 2, 3081, 328, 3110, 383)

        MegamanJump = New CArrFrame
        MegamanJump.Insert(2615, 244, 2593, 200, 2636, 292, 10, 2600, 214, 2629, 274)

        MegamanJumpDown = New CArrFrame
        MegamanJumpDown.Insert(2720, 242, 2693, 200, 2747, 292, 10, 2709, 214, 2736, 283)

        MegamanShoot = New CArrFrame
        MegamanShoot.Insert(2506, 244, 2474, 220, 2538, 292, 10, 2490, 228, 2523, 288)

        MegamanWalkShoot = New CArrFrame
        MegamanWalkShoot.Insert(2440, 457, 2426, 436, 2484, 502, 2, 2434, 436, 2466, 500)
        MegamanWalkShoot.Insert(2551, 457, 2534, 432, 2598, 502, 2, 2547, 436, 2578, 498)
        MegamanWalkShoot.Insert(2690, 457, 2662, 431, 2737, 500, 2, 2689, 436, 2718, 494)
        MegamanWalkShoot.Insert(2827, 454, 2811, 430, 2872, 500, 2, 2823, 435, 2853, 494)
        MegamanWalkShoot.Insert(2934, 454, 2916, 428, 2981, 500, 2, 2930, 432, 2960, 492)
        MegamanWalkShoot.Insert(3032, 454, 3010, 428, 3083, 500, 2, 3029, 436, 3065, 492)

        MegamanJumpShoot = New CArrFrame
        MegamanJumpShoot.Insert(2830, 245, 2810, 198, 2866, 292, 10, 2821, 214, 2845, 278)

        MegamanElectrocute = New CArrFrame
        MegamanElectrocute.Insert(2778, 556, 2741, 518, 2815, 608, 10, 2758, 538, 2803, 600)

        MegamanDeath = New CArrFrame
        MegamanDeath.Insert(2679, 556, 2644, 531, 2714, 602, 2, 2660, 538, 2702, 600)
        MegamanDeath.Insert(0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0)


        MM = New CCharMegaman
        ReDim MM.ArrSprites(9)
        MM.ArrSprites(0) = MegamanIntro
        MM.ArrSprites(1) = MegamanStand
        MM.ArrSprites(2) = MegamanWalk
        MM.ArrSprites(3) = MegamanJump
        MM.ArrSprites(4) = MegamanShoot
        MM.ArrSprites(5) = MegamanWalkShoot
        MM.ArrSprites(6) = MegamanJumpShoot
        MM.ArrSprites(7) = MegamanElectrocute
        MM.ArrSprites(8) = MegamanDeath
        MM.ArrSprites(9) = MegamanJumpDown


        MM.PosX = 75
        MM.PosY = 325
        MM.Vx = 0
        MM.Vy = 0
        MM.State(StateMegaman.Intro, 0)
        MM.FDir = FaceDir.Left
        ListChar.Add(MM)

        TigerProjCreate = New CArrFrame
        TigerProjCreate.Insert(1209, 1561, 1198, 1549, 1222, 1574, 5)
        TigerProjCreate.Insert(1290, 1561, 1275, 1547, 1304, 1575, 5)
        TigerProjCreate.Insert(1364, 1561, 1356, 1552, 1375, 1572, 5)

        MegamanProjCreate = New CArrFrame
        MegamanProjCreate.Insert(2493, 579, 2483, 570, 2503, 588, 1)

        bmp = New Bitmap(Img.Width, Img.Height)


        DisplayImg()
        ResizeImg()




        Timer1.Enabled = True
    End Sub

    Sub NTcollide() 'Sub for checking collison of Neon tiger
        If List_MP IsNot Nothing Then 'Check projectile collision
            For Each MP In List_MP
                If Not MP.Destroy Then
                    If MP.HitRight >= NT.HitLeft And MP.HitLeft <= NT.HitRight And MP.HitTop <= NT.HitBottom And MP.HitBottom >= NT.HitTop Then
                        If Not NT.Invulnerable Then
                            If NT.CurrState = StateNeonTiger.ShotBlock Then
                                If NT.FDir = FaceDir.Left And MP.PosX < NT.PosX Then
                                    MP.Vx = -10
                                    MP.Vy = -3
                                ElseIf NT.FDir = FaceDir.Right And MP.PosX > NT.PosX Then
                                    MP.Vx = 10
                                    MP.Vy = -3
                                Else
                                    NT.State(StateNeonTiger.FallDown, 9)
                                    MP.Destroy = True
                                End If
                            Else
                                NT.State(StateNeonTiger.FallDown, 9)
                                MP.Destroy = True
                            End If

                        End If
                        If NT.Invulnerable And Not (NT.CurrState = StateNeonTiger.ShotBlock) Then
                            MP.Destroy = True
                        End If
                    End If
                End If
            Next
        End If
        'Check Body collision
        If NT.CurrState = StateNeonTiger.RushAttGround And NT.HitRight >= MM.HitLeft And NT.HitLeft <= MM.HitRight And NT.HitTop <= MM.HitBottom And NT.HitBottom >= MM.HitTop Then
            NT.State(StateNeonTiger.MeleeCombo, 14)
        End If

    End Sub

    Sub MMcollide() 'Sub for checking collison of Megaman
        If List_TP IsNot Nothing Then 'Check projectile collision
            For Each TP In List_TP
                If Not TP.Destroy Then
                    If TP.HitRight >= MM.HitLeft And TP.HitLeft <= MM.HitRight And TP.HitTop <= MM.HitBottom And TP.HitBottom >= MM.HitTop Then
                        If Not MM.Invulnerable Then
                            MM.State(StateMegaman.Electrocute, 7)
                        End If
                        TP.Destroy = True
                    End If
                End If
            Next
        End If
        'Check body collision
        If NT.HitRight >= MM.HitLeft And NT.HitLeft <= MM.HitRight And NT.HitTop <= MM.HitBottom And NT.HitBottom >= MM.HitTop Then
            If Not MM.Invulnerable Then
                MM.State(StateMegaman.Death, 8)
            End If

        End If
    End Sub

    Sub PutSprites()
        Dim cc As CCharacter

        For i = 0 To Img.Width - 1
            For j = 0 To Img.Height - 1
                Img.Elmt(i, j) = Bg.Elmt(i, j)
            Next
        Next


        For Each cc In ListChar
            Dim EF As CElmtFrame = cc.ArrSprites(cc.IdxArrSprites).Elmt(cc.FrameIdx)
            Dim spritewidth = EF.Right - EF.Left
            Dim spriteheight = EF.Bottom - EF.Top
            If cc.FDir = FaceDir.Left Then

                Dim spriteleft As Integer = cc.PosX - EF.CtrPoint.x + EF.Left
                Dim spritetop As Integer = cc.PosY - EF.CtrPoint.y + EF.Top

                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Dim ImgX = spriteleft + i
                        Dim ImgY = spritetop + j
                        If ImgX >= 0 And ImgX <= Img.Width - 1 And ImgY >= 0 And ImgY <= Img.Height - 1 Then
                            Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), SpriteMask.Elmt(EF.Left + i, EF.Top + j))
                        End If
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Dim ImgX = spriteleft + i
                        Dim ImgY = spritetop + j
                        If ImgX >= 0 And ImgX <= Img.Width - 1 And ImgY >= 0 And ImgY <= Img.Height - 1 Then
                            Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), SpriteMap.Elmt(EF.Left + i, EF.Top + j))
                        End If
                    Next
                Next
            Else 'facing right
                Dim spriteleft = cc.PosX + EF.CtrPoint.x - EF.Right
                Dim spritetop = cc.PosY - EF.CtrPoint.y + EF.Top
                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Dim ImgX = spriteleft + i
                        Dim ImgY = spritetop + j
                        If ImgX >= 0 And ImgX <= Img.Width - 1 And ImgY >= 0 And ImgY <= Img.Height - 1 Then
                            Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), SpriteMask.Elmt(EF.Right - i, EF.Top + j))
                        End If
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Dim ImgX = spriteleft + i
                        Dim ImgY = spritetop + j
                        If ImgX >= 0 And ImgX <= Img.Width - 1 And ImgY >= 0 And ImgY <= Img.Height - 1 Then
                            Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), SpriteMap.Elmt(EF.Right - i, EF.Top + j))
                        End If
                    Next
                Next

            End If

        Next


    End Sub

    Sub DisplayImg()
        Dim i, j As Integer

        PutSprites()

        Dim rect As New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim bmpdata As System.Drawing.Imaging.BitmapData = bmp.LockBits(rect,
     System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat)

        Dim ptr As IntPtr = bmpdata.Scan0

        Dim bytes As Integer = Math.Abs(bmpdata.Stride) * bmp.Height
        Dim rgbvalues(bytes) As Byte

        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbvalues, 0, bytes)

        Dim n As Integer = 0
        Dim col As System.Drawing.Color

        For j = 0 To Img.Height - 1
            For i = 0 To Img.Width - 1
                col = Img.Elmt(i, j)
                rgbvalues(n) = col.B
                rgbvalues(n + 1) = col.G
                rgbvalues(n + 2) = col.R
                rgbvalues(n + 3) = col.A

                n = n + 4
            Next
        Next

        System.Runtime.InteropServices.Marshal.Copy(rgbvalues, 0, ptr, bytes)

        bmp.UnlockBits(bmpdata)

        PictureBox1.Refresh()

        PictureBox1.Image = bmp
        PictureBox1.Width = bmp.Width
        PictureBox1.Height = bmp.Height
        PictureBox1.Top = 0
        PictureBox1.Left = 0

    End Sub



    Sub ResizeImg()
        Dim w, h As Integer

        w = PictureBox1.Width
        h = PictureBox1.Height

        Me.ClientSize = New Size(w, h)

    End Sub




    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim CC As CCharacter
        PictureBox1.Refresh()
        If MM.Destroy Then
            Respawn += 1
        End If
        If Respawn = 20 Then
            Respawn = 0
            MM.Destroy = False
            Dim Position = Math.Floor((Rnd() * 390) + 55)
            While Position < NT.PosX + 100 And Position > NT.PosX - 100
                Position = Math.Floor((Rnd() * 390) + 55)
            End While
            MM.PosX = Position
            If MM.PosX < NT.PosX Then
                MM.FDir = FaceDir.Left
            Else
                MM.FDir = FaceDir.Right
            End If
            ListChar.Add(MM)
            MM.State(StateMegaman.Intro, 0)
        End If
        NTcollide()
        MMcollide()
        For Each CC In ListChar
            CC.Update()
        Next

        If NT.CurrState = StateNeonTiger.RaySplashWall And NT.CurrFrame = 2 And NT.FrameIdx = 7 Then
            CreateTigerProjectile(1)
        ElseIf NT.CurrState = StateNeonTiger.RaySplashGround And NT.CurrFrame = 2 And NT.FrameIdx = 8 Then
            CreateTigerProjectile(2)
        End If

        If MM.CurrState = StateMegaman.Shoot And MM.CurrFrame = 1 And MM.FrameIdx = 0 Then
            CreateMegamanProjectile(1)
        ElseIf MM.CurrState = StateMegaman.WalkShoot And MM.CurrFrame = 1 And MM.FrameIdx = 0 Then
            CreateMegamanProjectile(2)
        ElseIf MM.CurrState = StateMegaman.JumpShoot And MM.CurrFrame = 1 And MM.FrameIdx = 0 Then
            CreateMegamanProjectile(2)
        End If

        Dim Listchar1 As New List(Of CCharacter)

        For Each CC In ListChar
            If Not CC.Destroy Then
                Listchar1.Add(CC)
            End If
        Next

        ListChar = Listchar1

        DisplayImg()

    End Sub

    Sub CreateTigerProjectile(n As Integer)
        TP = New CCharTigerProjectile
        If n = 1 Then
            If NT.FDir = FaceDir.Left Then
                TP.PosX = NT.PosX - 55
                TP.Vx = -10
            Else
                TP.PosX = NT.PosX + 45
                TP.Vx = 10
            End If
            TP.PosY = NT.PosY - 14
        Else
            If NT.FDir = FaceDir.Left Then
                TP.PosX = NT.PosX - 70
                TP.Vx = -10
            Else
                TP.PosX = NT.PosX + 70
                TP.Vx = 10
            End If
            TP.PosY = NT.PosY + 17
        End If

        Dim random = Rnd()
        If random < 0.2 Then
            TP.Vy = -5
        ElseIf random < 0.4 Then
            TP.Vy = -3
        ElseIf random < 0.6 Then
            TP.Vy = 0
        ElseIf random < 0.8 Then
            TP.Vy = 3
        Else
            TP.Vy = 5
        End If

        TP.CurrState = StateTigerProjectile.Create
        ReDim TP.ArrSprites(1)
        TP.ArrSprites(0) = TigerProjCreate
        List_TP.Add(TP)
        ListChar.Add(TP)
    End Sub

    Sub CreateMegamanProjectile(n As Integer)
        MP = New CCharMegaProjectile
        If n = 1 Then
            If MM.FDir = FaceDir.Left Then
                MP.PosX = MM.PosX + 30
                MP.Vx = 10
            Else
                MP.PosX = MM.PosX - 30
                MP.Vx = -10
            End If
            MP.PosY = MM.PosY + 7
        ElseIf n = 2 Then
            If MM.FDir = FaceDir.Left Then
                MP.PosX = MM.PosX + 48
                MP.Vx = 10
            Else
                MP.PosX = MM.PosX - 48
                MP.Vx = -10
            End If
            MP.PosY = MM.PosY + 4
        End If


        MP.CurrState = StateMegamanProjectile.Create
        ReDim MP.ArrSprites(1)
        MP.ArrSprites(0) = MegamanProjCreate
        List_MP.Add(MP)
        ListChar.Add(MP)
    End Sub


    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown 'Sub for user controls
        If e.KeyCode = Keys.Escape Then
            helpForm.Show()
        End If
        Select Case NT.CurrState 'Neon Tiger control
            Case StateNeonTiger.Stand
                If e.KeyCode = Keys.W Then
                    NT.State(StateNeonTiger.JumpStart, 2)
                ElseIf e.KeyCode = Keys.A Then
                    NT.FDir = FaceDir.Left
                ElseIf e.KeyCode = Keys.D Then
                    NT.FDir = FaceDir.Right
                ElseIf e.KeyCode = Keys.Q Then
                    NT.State(StateNeonTiger.RushAttGround, 12)
                ElseIf e.KeyCode = Keys.E Then
                    NT.State(StateNeonTiger.RaySplashGround, 10)
                ElseIf e.KeyCode = Keys.R Then
                    NT.State(StateNeonTiger.ShotBlock, 11)
                End If
            Case StateNeonTiger.ClingToWall
                If e.KeyCode = Keys.W Then
                    NT.State(StateNeonTiger.JumpUpForward, 3)
                ElseIf e.KeyCode = Keys.E Then
                    NT.State(StateNeonTiger.RaySplashWall, 7)
                ElseIf e.KeyCode = Keys.Q And NT.PosY < 260 Then
                    Dim x, t, h As Integer
                    NT.Vy = 5
                    Select Case NT.FDir
                        Case FaceDir.Left
                            x = NT.PosX - MM.PosX
                        Case FaceDir.Right
                            x = MM.PosX - NT.PosX
                    End Select
                    h = 325 - NT.PosY
                    t = h / NT.Vy
                    NT.Vx = x / t
                    NT.State(StateNeonTiger.DiveAtt, 8)
                ElseIf e.KeyCode = Keys.S Then
                    NT.State(StateNeonTiger.JumpDown, 4)
                End If
        End Select

        Select Case MM.CurrState 'Megaman Control
            Case StateMegaman.Stand
                If e.KeyCode = Keys.J Then
                    MM.State(StateMegaman.Walk, 2)
                    MM.FDir = FaceDir.Right
                    MM.Vx = -5
                ElseIf e.KeyCode = Keys.L Then
                    MM.State(StateMegaman.Walk, 2)
                    MM.FDir = FaceDir.Left
                    MM.Vx = 5
                ElseIf e.KeyCode = Keys.O Then
                    MM.State(StateMegaman.Shoot, 4)
                ElseIf e.KeyCode = Keys.I Then
                    MM.State(StateMegaman.Jump, 3)
                    MM.Vy = -14
                End If
            Case StateMegaman.Walk
                If e.KeyCode = Keys.O Then
                    MM.State(StateMegaman.WalkShoot, 5)
                ElseIf e.KeyCode = Keys.I Then
                    MM.State(StateMegaman.Jump, 3)
                    MM.Vy = -14
                End If
            Case StateMegaman.Shoot
                If e.KeyCode = Keys.J Then
                    MM.FDir = FaceDir.Right
                ElseIf e.KeyCode = Keys.L Then
                    MM.FDir = FaceDir.Left
                End If
            Case StateMegaman.Jump
                If e.KeyCode = Keys.O Then
                    MM.State(StateMegaman.JumpShoot, 6)
                End If
                If e.KeyCode = Keys.J Then
                    MM.FDir = FaceDir.Right
                    MM.Vx = -5
                ElseIf e.KeyCode = Keys.L Then
                    MM.FDir = FaceDir.Left
                    MM.Vx = 5
                End If
            Case StateMegaman.JumpShoot
                If e.KeyCode = Keys.J Then
                    MM.FDir = FaceDir.Right
                    MM.Vx = -5
                ElseIf e.KeyCode = Keys.L Then
                    MM.FDir = FaceDir.Left
                    MM.Vx = 5
                End If
            Case StateMegaman.JumpDown
                If e.KeyCode = Keys.O Then
                    MM.State(StateMegaman.JumpShoot, 6)
                ElseIf e.KeyCode = Keys.J Then
                    MM.FDir = FaceDir.Right
                    MM.Vx = -5
                ElseIf e.KeyCode = Keys.L Then
                    MM.FDir = FaceDir.Left
                    MM.Vx = 5
                End If
        End Select
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.J Or e.KeyCode = Keys.L Then
            If MM.PosY = 325 And Not (MM.CurrState = StateMegaman.Death Or MM.CurrState = StateMegaman.Electrocute) Then
                MM.State(StateMegaman.Stand, 1)
            End If
            MM.Vx = 0
        ElseIf NT.CurrState = StateNeonTiger.ShotBlock Then
            If e.KeyCode = Keys.R Then
                NT.State(StateNeonTiger.Stand, 1)
            End If
        End If
    End Sub

    Private Sub helpBox_Click(sender As Object, e As EventArgs) Handles helpBox.Click
        helpForm.Show()
    End Sub
End Class
