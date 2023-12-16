﻿namespace Day7
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var hands = ParseGame(_input).ToList();

      var handsByType = new Dictionary<int, List<Hand>>();

      foreach (var hand in hands)
      {
        var handType = GetHandType(hand);
        if(handsByType.TryGetValue(handType, out var handsOfType))
        {
          handsOfType.Add(hand);
        }
        else
        {
          handsByType[handType] = new List<Hand>() { hand };
        }
      }

      var result = 0;
      var rank = 0;
      foreach (var handsOfType in handsByType.OrderBy(kvp => kvp.Key))
      {
        handsOfType.Value.Sort();

        foreach (var hand in handsOfType.Value)
        {
          result += hand.Bid * ++rank;
        }
      }

      Console.WriteLine(result);
    }

    private static int GetHandType(Hand hand)
    {
      var group = hand.Cards.GroupBy(i => i).ToList();
      if (IsFiveOfKind(group))
      {
        return 7;
      }
      
      if (IsFourOfKind(group))
      {
        return 6;
      }

      if (IsFullHouse(group))
      {
        return 5;
      }

      if (IsThreeOfKind(group))
      {
        return 4;
      }

      if (IsTwoPair(group))
      {
        return 3;
      }

      if (IsOnePair(group))
      {
        return 2;
      }

      if (IsOnePair(group))
      {
        return 1;
      }

      return 0;
    }

    private static bool IsFiveOfKind(List<IGrouping<char, char>> hand)
    {
      return hand.Count == 1;
    }

    private static bool IsFourOfKind(List<IGrouping<char, char>> hand)
    {
      return hand.Count == 2 && hand.Any(grp => grp.Count() == 4);
    }

    private static bool IsFullHouse(List<IGrouping<char, char>> hand)
    {
      return hand.Count == 2 && hand .Any(grp => grp.Count() == 2);
    }

    private static bool IsThreeOfKind(List<IGrouping<char, char>> hand)
    {
      return hand.Count == 3 && hand.Any(grp => grp.Count() == 3);
    }

    private static bool IsTwoPair(List<IGrouping<char, char>> hand)
    {
      return hand.Count == 3 && hand.Any(grp => grp.Count() == 2) && hand.Any(grp => grp.Count() == 1);
    }

    private static bool IsOnePair(List<IGrouping<char, char>> hand)
    {
      return hand.Count == 4 && hand.Any(grp => grp.Count() == 2);
    }

    private static bool IsHighCard(List<IGrouping<char, char>> hand)
    {
      return hand.Count == 5;
    }

    private static IEnumerable<Hand> ParseGame(string input)
    {
      var lines = input.Split(Environment.NewLine);
      foreach (var line in lines)
      {
        var hand = line.Split(" ");
        yield return new Hand(hand[0], int.Parse(hand[1]));
      }
    }

    private record Hand(string Cards, int Bid) : IComparable<Hand>
    {
      public int CompareTo(Hand? other)
      {
        for(var i = 0; i < Cards.Length; i++)
        {
          var card = Cards[i];
          var otherCard = other!.Cards[i];

          var strOfCard = GetStrOfCard(card);
          var strOfOtherCard = GetStrOfCard(otherCard);

          if(strOfCard > strOfOtherCard)
          {
            return 1;
          }
          else if (strOfCard < strOfOtherCard)
          {
            return -1;
          }
        }

        return 0;
      }

      private int GetStrOfCard(char card)
      {
        return _cardToStr[card];
      }
    }

    private static Dictionary<char, int> _cardToStr = new Dictionary<char, int>()
    {
      { '2', 1 },
      { '3', 2 },
      { '4', 3 },
      { '5', 4 },
      { '6', 5 },
      { '7', 6 },
      { '8', 7 },
      { '9', 8 },
      { 'T', 9 },
      { 'J', 10 },
      { 'Q', 11 },
      { 'K', 12 },
      { 'A', 13 },
    };

    internal const string _testInput = """
      32T3K 765
      T55J5 684
      KK677 28
      KTJJT 220
      QQQJA 483
      """;

    internal const string _input = """
      536K8 291
      3T3T3 802
      5872Q 265
      K98Q4 232
      292A9 349
      825JJ 785
      68K48 521
      94A44 358
      J8JJ3 490
      7KJA8 510
      K786T 501
      QQ968 560
      8Q58J 395
      JAAJA 308
      8T7TA 2
      T5T56 216
      6Q877 398
      949QT 406
      AKJ93 400
      JK22J 414
      6AA6A 384
      68666 820
      99A99 72
      8884J 273
      97888 881
      7727T 421
      T3A43 942
      QTQTT 429
      44J29 244
      QKKKQ 537
      KK99K 51
      3333A 307
      A8AJA 721
      J7297 731
      544Q7 770
      73337 562
      226Q6 34
      5A666 857
      Q88J4 460
      97724 586
      ATA77 444
      42TTT 930
      76667 193
      43443 538
      K9994 927
      488K8 153
      33223 798
      T999A 831
      3983T 819
      227JJ 977
      79999 924
      2AQ93 778
      QQQQ4 573
      TQTQJ 367
      8A8AA 68
      44J24 279
      Q6AAQ 74
      JAQK4 709
      J355T 867
      J6666 773
      6AA63 593
      88485 626
      66965 368
      88JKJ 412
      T4T7T 467
      2A2A2 482
      77Q8Q 95
      6Q685 178
      KJ7Q3 812
      5892A 536
      837K7 266
      TQQ44 165
      QQJ73 868
      7A779 494
      77776 231
      8K264 504
      55552 900
      4AQK8 425
      QJQQ5 257
      3222T 945
      92299 603
      AK799 751
      7T5Q9 209
      88J8J 484
      TAJA9 992
      TT2TT 962
      A477A 864
      859JA 995
      J3A7A 156
      99977 580
      J5522 391
      Q6464 465
      85888 515
      7J272 954
      K6T6T 461
      TT797 534
      8Q48K 990
      69898 342
      Q979J 332
      666TT 840
      82522 793
      J888K 730
      4JJ8K 667
      AK893 415
      2992J 622
      Q74QQ 591
      9AQJ6 195
      9TT9T 409
      66JQ6 735
      33A7J 60
      5QA89 643
      49A99 327
      97JJ9 131
      2K86J 1000
      66557 320
      65666 117
      AQQQQ 362
      639K9 727
      T9996 418
      7J773 167
      J2883 666
      683Q8 697
      2T228 984
      J2332 250
      8J882 258
      47T38 397
      QQQ44 130
      73ATQ 53
      94494 685
      8263J 157
      5A228 89
      56456 462
      KK2KK 48
      9QQKK 448
      Q8878 470
      82J77 294
      35735 952
      84838 387
      9K2AT 592
      AJ999 815
      99J99 539
      KKKK8 443
      77969 674
      55556 65
      TTJJA 738
      KK4K4 599
      5J5K6 162
      88555 797
      9J89K 411
      K58K5 47
      TJQ2T 477
      2A324 401
      66K66 854
      668T6 822
      9J9K9 579
      93AQ6 662
      J7693 755
      JT63A 430
      55536 242
      698K8 369
      54544 211
      884A4 161
      4J999 713
      47466 800
      9T57K 519
      34TT4 883
      44JJ4 476
      99272 789
      76862 432
      7TT4Q 378
      3K724 918
      A6JT5 931
      885J5 380
      38888 636
      J55J9 687
      AJ4QJ 627
      372AT 681
      9QQ3Q 253
      3A433 334
      AQTK6 936
      44QQ4 825
      J2J77 309
      8T88A 858
      39323 393
      T4994 428
      259K6 208
      66A6A 568
      2K8K2 966
      423KJ 587
      42884 263
      977T9 363
      K33K3 493
      726T4 782
      T5396 948
      82833 878
      3636J 873
      72572 640
      2975T 670
      5Q57Q 832
      TK39A 506
      JTK9T 916
      5333T 968
      TQKQK 183
      3JAJ2 631
      5Q5Q5 191
      35Q3A 351
      J5AT5 823
      77747 249
      3QQQ4 399
      7J3KA 427
      K77J7 52
      946Q5 112
      T996Q 464
      7J757 876
      A9AKA 483
      JQK82 238
      6J45K 641
      AAAAQ 453
      4AAAA 704
      23A95 512
      23Q57 979
      75777 347
      27QK9 806
      K5K76 126
      KTTKT 79
      76945 522
      J7793 442
      7865T 433
      53Q2K 481
      Q5Q99 339
      5T2JJ 124
      93353 928
      99JAT 673
      85T96 839
      A7777 49
      TTT65 898
      J6234 618
      99594 891
      448J8 748
      2J55T 722
      59575 164
      45745 4
      5TT53 35
      QJ36A 469
      57752 502
      QQQ5Q 222
      72QQ4 13
      846AQ 611
      96JKT 102
      AJ672 693
      3754J 774
      8499Q 321
      A6K4A 93
      6724Q 694
      J88T8 174
      77A7J 548
      28828 671
      58J3J 886
      77J5J 389
      3KQKK 747
      96J9A 149
      94326 360
      A84J7 644
      AKAK8 116
      47724 498
      646A9 988
      QAQ9Q 39
      37Q77 635
      83J84 98
      3J6T6 316
      898AT 542
      TT33Q 872
      A4544 610
      465K8 540
      J229J 921
      QQQ77 86
      A393A 413
      A5T2T 894
      9TA33 264
      33354 459
      6Q44A 471
      23479 168
      78T77 875
      88T8T 902
      9KKKK 479
      23TQJ 612
      2A789 333
      35444 827
      J3T7Q 620
      78JQA 870
      KKKJK 816
      98888 114
      55755 240
      9JKTQ 972
      99786 436
      53JQ4 714
      365Q6 695
      A99JQ 684
      78Q48 297
      4J777 245
      778A3 795
      9TJ99 869
      66249 394
      63663 550
      8A82A 111
      88829 577
      6Q66Q 454
      5552A 608
      99JT7 340
      75376 169
      5QKKQ 843
      KQ49Q 582
      8934K 981
      64374 452
      7727Q 784
      5Q57K 500
      Q2277 525
      T423A 27
      2Q3K3 69
      8K2A7 554
      6KJ9Q 201
      68T45 455
      Q8989 318
      75A76 656
      88K29 563
      JQ268 396
      33777 653
      6J746 715
      89889 913
      7KJT6 85
      58437 26
      3J374 440
      A344A 96
      K3377 760
      422K2 892
      77676 219
      4844J 893
      86665 243
      J35K4 261
      6JJ66 669
      T8484 908
      J9A38 909
      K2689 57
      T4444 472
      TJ37T 301
      A44J4 210
      4Q3A6 306
      746T8 826
      88TTT 765
      T695J 761
      22255 783
      Q8KKJ 808
      86323 561
      58A64 997
      8888J 214
      KJKTK 595
      68484 235
      7QT7Q 617
      39J39 159
      JJ8J8 682
      4T57J 233
      AJ99A 81
      Q28T6 488
      3J2J9 706
      6A356 382
      AJA77 300
      26424 889
      QAA99 584
      897K7 28
      Q29A6 71
      TT366 144
      42898 767
      77A7A 247
      2KT23 941
      72229 555
      3245A 381
      7AQT6 434
      22AAJ 845
      333AJ 926
      A4777 897
      22772 298
      J3838 225
      34K5A 734
      AQ7Q9 423
      T27TJ 33
      T5352 417
      326K6 851
      83J73 754
      454J4 594
      K7KK7 70
      AKQT3 964
      44834 907
      94889 724
      J3J87 91
      Q26T5 955
      8Q888 370
      KK96K 277
      82826 344
      64696 492
      K553T 456
      8Q84Q 445
      TTT37 110
      9999Q 707
      A3282 719
      5KKKK 865
      2AA3A 325
      93993 239
      QQQAA 58
      289TK 824
      9595J 691
      954K2 326
      38AA5 63
      A37Q4 66
      TT7JT 230
      K44K4 982
      T8725 910
      KK5K9 986
      22J2J 290
      75Q69 353
      4444J 998
      2AK24 764
      K958K 138
      69999 78
      K66J6 686
      4TTT8 978
      99J88 466
      4Q28K 794
      K5Q68 198
      9J452 862
      66868 885
      Q5248 148
      Q2522 675
      AK576 523
      83KJT 376
      29A98 659
      35535 850
      54225 228
      85K55 634
      5394T 152
      4QQ42 814
      JAKAK 999
      ATTT8 632
      42K2K 650
      AKAK7 701
      TK653 588
      222TT 404
      Q7777 11
      9449Q 629
      A8K82 949
      33335 207
      KA6AQ 204
      Q4AQT 435
      63638 757
      A78TK 446
      7327Q 938
      JJJJJ 571
      TKKKK 505
      9474A 184
      A5T8K 55
      9QQ2A 286
      66669 969
      32992 692
      55J55 431
      AJTTT 810
      JQ369 365
      QJAKA 312
      3959Q 786
      AKKAK 528
      Q2Q28 25
      AKQKQ 678
      2266A 94
      234K5 246
      AA8AA 136
      64626 474
      2Q676 287
      TA33J 163
      K9KKT 468
      A345J 664
      JJ2JJ 920
      9KKJK 564
      JK6J8 278
      279Q2 408
      TJ777 683
      8KJ63 507
      2AAA2 917
      99499 274
      3AQ52 64
      JJJJ8 217
      77878 589
      KQ4A7 101
      8AQA5 186
      T5555 379
      TJT88 267
      8K888 129
      22KKT 292
      AAAQK 215
      26Q3J 170
      76679 192
      J9333 717
      JT99T 828
      A9887 503
      22922 139
      JKKJ4 759
      88J78 45
      94KKQ 637
      TT32K 280
      K45K4 752
      23244 348
      5K743 690
      8AQA8 566
      5JJAA 863
      TT8A3 50
      A6776 103
      3Q3Q3 108
      A95J3 106
      ATTAT 145
      83AJT 38
      JQJQQ 218
      66Q74 84
      77875 424
      J844J 597
      27Q8K 172
      8QQQ5 357
      52QJJ 991
      66555 965
      393T9 495
      7453T 766
      K65KK 441
      8A78A 223
      33635 92
      4K244 533
      95955 337
      659Q2 329
      7JQQK 372
      9A99A 605
      7JKAK 100
      923T7 652
      74444 939
      39776 529
      5T79T 710
      26662 275
      9TT8T 226
      7K22K 837
      37829 609
      773JJ 29
      QAAJA 335
      8TTTT 996
      AJAA2 352
      47559 552
      46AJJ 957
      55K55 8
      J267K 30
      594A6 788
      6JQ67 657
      44QK3 76
      88448 558
      7777K 601
      J9J99 663
      88833 585
      K26K2 700
      Q63JT 676
      28525 392
      777JJ 237
      4KK8J 346
      66366 166
      AKKK2 744
      AK7AJ 711
      KAAAA 288
      A5757 132
      29835 107
      754A4 805
      JQKA7 187
      Q88KJ 151
      439KT 82
      93J99 343
      66683 654
      28227 882
      JJ555 366
      JJ992 17
      4TAKA 416
      Q7T43 677
      778J8 109
      J6662 386
      38TTT 31
      53Q3Q 821
      JJ66J 648
      44A4A 80
      TTTT4 852
      88887 311
      52K74 177
      77746 310
      KA444 787
      66266 42
      99JQ9 660
      Q3A9K 590
      6636J 113
      4Q234 855
      J377Q 289
      99992 313
      9Q985 547
      J6959 959
      6K3AJ 905
      355A5 884
      6K5Q2 426
      36KKJ 83
      87777 749
      84A9T 572
      2KKJ2 105
      4J446 385
      Q3333 901
      8535Q 912
      T8868 638
      39999 842
      2Q7JQ 296
      T7Q6K 614
      37357 613
      T422T 647
      K6TTQ 943
      37686 565
      Q99A9 439
      59J99 624
      85A3T 628
      T3T25 438
      K2T72 874
      TTJTT 324
      4JK42 405
      T6T26 625
      3JKKT 205
      JKKJK 227
      87J74 725
      KKKK3 518
      KQ8A9 768
      AAAA9 736
      542TK 847
      33363 268
      989KK 77
      64T9K 771
      38KJK 578
      7333J 137
      TT3T6 746
      86766 419
      KKKKQ 817
      AJTK5 642
      5TK55 37
      KQT6Q 62
      K66Q6 480
      QQ449 958
      KQK44 803
      226K6 141
      Q8Q8Q 762
      84448 877
      95977 14
      Q266A 947
      47K93 374
      574T6 835
      57888 732
      QAK34 769
      67699 388
      9A9AA 373
      QQ23Q 809
      J4QKQ 12
      6666T 350
      J838T 922
      7A775 23
      73433 526
      32255 792
      T3J3J 75
      K656K 741
      K9447 556
      QJQ9Q 801
      4J545 32
      4JK87 314
      7K7KT 173
      TT9A2 739
      6QQ6A 698
      67JA6 546
      3J94T 929
      588AQ 248
      JKA44 950
      33878 983
      54484 569
      7777J 447
      T4JA2 811
      77495 919
      K3T33 606
      AK7AA 903
      KQ96T 229
      7QTJQ 545
      Q453T 776
      TTT3T 190
      JAKKT 450
      K9J94 985
      72787 520
      5QJ86 848
      9QQ9Q 202
      2TTJT 383
      23A33 703
      KT2KT 781
      88333 181
      95299 236
      J889A 356
      Q7336 302
      A2AJQ 175
      68TAJ 976
      T6J33 176
      T97K9 914
      363K6 281
      A2579 975
      67897 203
      555JT 147
      KT2T2 777
      J2524 541
      5K238 19
      55JA5 128
      ATA22 20
      2J222 364
      57T7T 860
      45922 276
      99A96 544
      K7KKK 689
      4278A 322
      78QQQ 234
      999TQ 602
      7J989 299
      6J468 3
      5K445 887
      64A57 559
      JK2TQ 221
      6A323 87
      QQ5Q6 607
      4448Q 271
      T3333 937
      74477 402
      99TT9 284
      J83J8 696
      AK99K 600
      2AT62 473
      88755 119
      QQ2QQ 796
      K85K2 543
      4K4J4 818
      JTQ2Q 407
      TT767 56
      67376 956
      59656 726
      K8KJ8 830
      55JTT 933
      333A7 194
      234T8 527
      74ATA 146
      7777T 799
      JA33A 596
      K7444 575
      3676J 973
      76227 104
      5TJKT 646
      868K8 630
      A6TAA 422
      JQK55 43
      548Q5 829
      4JK9Q 557
      J88AA 154
      9JQ76 403
      75899 61
      QQQTT 361
      27664 517
      A58JA 123
      32A32 514
      J3AJK 953
      55558 961
      366Q3 99
      22QQ2 772
      64646 633
      693KA 143
      66A76 21
      384KQ 475
      Q88Q8 142
      922KK 980
      3K33J 345
      AATAT 866
      93T5Q 598
      24222 716
      6K6A6 449
      3JQ3Q 135
      AJ855 888
      8QT46 260
      5Q7TK 895
      2J2K2 615
      55955 293
      539T5 9
      TJTJT 158
      QQ2J2 341
      QJQQ6 915
      K24K9 121
      TTTT6 390
      35533 906
      4TKAQ 904
      K22KQ 241
      Q422Q 688
      JJ333 804
      K8K5J 745
      44662 896
      4A93J 763
      4QJQ9 720
      Q4J36 256
      44T47 853
      QQQQ3 67
      8539T 661
      555Q5 496
      99696 516
      K7K97 5
      QQ677 923
      J892Q 679
      T4T47 649
      8A888 699
      79228 665
      898Q7 328
      5JKQJ 934
      K27K8 790
      799T2 196
      J2JTT 639
      4Q872 993
      5494Q 672
      68Q88 122
      82KK6 497
      QQ64T 775
      4J55Q 120
      KKKK4 524
      K8KK2 371
      6J6J4 478
      6QQQQ 743
      25QJQ 259
      9Q33Q 574
      999K9 750
      K4Q2K 463
      KT7TT 974
      2Q82K 621
      T77QA 833
      J6656 723
      AJ596 487
      A9Q98 206
      2A4K5 485
      9929J 377
      J59K6 336
      9JQ58 10
      JQQ66 46
      KK77A 220
      8K88K 570
      76T7J 185
      986T7 807
      44TJT 836
      J8662 283
      93457 619
      5K5A6 269
      K92T7 15
      3J295 16
      A5556 951
      J4KAA 742
      KKKJ7 791
      9Q879 118
      TKT33 946
      KK5K2 645
      83T4K 41
      K7Q2J 712
      99Q9Q 451
      58355 491
      6Q7T8 680
      5T3TT 199
      Q577A 188
      KTK3T 551
      Q664Q 359
      49383 22
      QT626 354
      T6TT6 73
      724K9 737
      AAA7A 530
      3KAA3 375
      3KK3K 994
      9A992 133
      QK5K7 971
      QJQ26 88
      82782 989
      TA59Q 967
      33233 553
      KQ246 963
      5J389 1
      333J3 270
      ATQQQ 150
      A77TT 140
      KK525 182
      TA4J8 871
      2T92T 262
      8K6A5 576
      9JJ36 319
      2Q883 970
      79877 134
      6J676 940
      55459 838
      43444 756
      48945 655
      KJ997 295
      9Q65K 623
      34KQT 879
      38282 549
      T892Q 844
      Q8KAK 849
      8TT68 315
      33AAA 338
      3TTT3 944
      TTTQT 718
      44TA9 733
      396Q3 780
      5A427 18
      TQAKJ 668
      99J79 658
      8833Q 513
      TTTAT 304
      T5558 252
      9T999 960
      JQQQQ 331
      888J3 255
      JQQ8J 753
      T5554 282
      A4928 90
      48TT7 303
      K889Q 457
      63A3A 44
      97935 224
      6K5A9 583
      AJ955 531
      78448 251
      QJT99 200
      T344J 708
      TT65J 813
      K999K 758
      4T634 486
      78JQ8 36
      9J86J 859
      94JJ9 189
      Q4JQ4 458
      6438A 180
      3645K 509
      T22J2 7
      9K992 272
      JAAAT 581
      AA262 197
      K996K 127
      99899 254
      55TTT 729
      4T9JA 834
      TAA5A 125
      JQ925 511
      9K895 846
      89899 305
      7J787 59
      77977 651
      J5A49 616
      Q3993 323
      3A3TT 567
      89388 212
      KKKK6 740
      8T79T 779
      A7AA7 702
      QQ2Q2 880
      48396 6
      33373 155
      9K73T 932
      KJTJT 728
      TK424 935
      64666 160
      3K337 499
      76337 890
      AAAA2 420
      KAJ32 987
      3K333 925
      66655 911
      QK54J 437
      7JJKA 40
      Q5Q5Q 489
      QQ84Q 355
      8K66K 330
      QTTTJ 899
      JJQJQ 171
      4688T 54
      AAAAJ 604
      5K6KJ 24
      54554 856
      K2K2K 535
      A5AAA 285
      385JK 213
      64T89 705
      K4K46 841
      288Q9 410
      39AAJ 97
      9Q48A 115
      8448K 861
      Q33QQ 508
      J8A2A 317
      A92AJ 179
      2222Q 532
      """;
  }
}