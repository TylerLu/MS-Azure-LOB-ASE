﻿/*   
 *   * Copyright (c) Microsoft Corporation. All rights reserved. Licensed under the MIT license.  
 *   * See LICENSE in the project root for license information.  
 */

// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Data.Migrations.Sales
{
    [DbContext(typeof(SalesContext))]
    [Migration("20180228074655_Intial")]
    partial class Intial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Entities.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BuyerId");

                    b.Property<DateTimeOffset>("OrderDate");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ApplicationCore.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OrderId");

                    b.Property<decimal>("UnitPrice");

                    b.Property<int>("Units");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Microsoft.eShopWeb.ApplicationCore.Entities.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BuyerId");

                    b.HasKey("Id");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Microsoft.eShopWeb.ApplicationCore.Entities.BasketItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BasketId");

                    b.Property<int>("CatalogItemId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.ToTable("BasketItem");
                });

            modelBuilder.Entity("ApplicationCore.Entities.OrderAggregate.Order", b =>
                {
                    b.OwnsOne("ApplicationCore.Entities.OrderAggregate.Address", "ShipToAddress", b1 =>
                        {
                            b1.Property<int>("OrderId");

                            b1.Property<string>("City");

                            b1.Property<string>("Country");

                            b1.Property<string>("State");

                            b1.Property<string>("Street");

                            b1.Property<string>("ZipCode");

                            b1.ToTable("Orders");

                            b1.HasOne("ApplicationCore.Entities.OrderAggregate.Order")
                                .WithOne("ShipToAddress")
                                .HasForeignKey("ApplicationCore.Entities.OrderAggregate.Address", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("ApplicationCore.Entities.OrderAggregate.Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.OwnsOne("ApplicationCore.Entities.OrderAggregate.CatalogItemOrdered", "ItemOrdered", b1 =>
                        {
                            b1.Property<int>("OrderItemId");

                            b1.Property<int>("CatalogItemId");

                            b1.Property<string>("PictureUri");

                            b1.Property<string>("ProductName");

                            b1.ToTable("OrderItems");

                            b1.HasOne("ApplicationCore.Entities.OrderAggregate.OrderItem")
                                .WithOne("ItemOrdered")
                                .HasForeignKey("ApplicationCore.Entities.OrderAggregate.CatalogItemOrdered", "OrderItemId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Microsoft.eShopWeb.ApplicationCore.Entities.BasketItem", b =>
                {
                    b.HasOne("Microsoft.eShopWeb.ApplicationCore.Entities.Basket")
                        .WithMany("Items")
                        .HasForeignKey("BasketId");
                });
#pragma warning restore 612, 618
        }
    }
}
