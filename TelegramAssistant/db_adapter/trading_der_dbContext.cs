using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;

namespace TelegramAssistant.db_adapter
{
    public partial class trading_der_dbContext : DbContext,  IHostedService
    {
        public trading_der_dbContext()
        {
        }

        public trading_der_dbContext(DbContextOptions<trading_der_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Deals> Deals { get; set; }
        public virtual DbSet<FuturesQuotes> FuturesQuotes { get; set; }
        public virtual DbSet<FuturesQuotesAccumulated> FuturesQuotesAccumulated { get; set; }
        public virtual DbSet<FuturesSpec> FuturesSpec { get; set; }
        public virtual DbSet<LimitsArch> LimitsArch { get; set; }
        public virtual DbSet<MatlabHestonHedgingCalc> MatlabHestonHedgingCalc { get; set; }
        public virtual DbSet<OptionsQuotes> OptionsQuotes { get; set; }
        public virtual DbSet<OptionsQuotesArch> OptionsQuotesArch { get; set; }
        public virtual DbSet<OptionsSpec> OptionsSpec { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Portfolio1> Portfolio1 { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<PositionsArch> PositionsArch { get; set; }
        public virtual DbSet<QuotesArchive> QuotesArchive { get; set; }
        public virtual DbSet<QuotesCandlesUpdatedFromTicks> QuotesCandlesUpdatedFromTicks { get; set; }

        // Unable to generate entity type for table 'quik.portfolio'. Please see the warning messages.
        // Unable to generate entity type for table 'quik.deals_arch'. Please see the warning messages.
        // Unable to generate entity type for table 'quik.limits'. Please see the warning messages.
        // Unable to generate entity type for table 'quik.opt_margin_info'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.quotes_raw'. Please see the warning messages.
        // Unable to generate entity type for table 'quik.deals'. Please see the warning messages.
        // Unable to generate entity type for table 'quik.positions'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=trading_der_db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Deals>(entity =>
            {
                entity.ToTable("deals", "smile");

                entity.HasIndex(e => new { e.IsVirtual, e.PortfolioId, e.CreateDate })
                    .HasName("ind_portid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasColumnName("account_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddInfo)
                    .HasColumnName("add_info")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.DealIdExch).HasColumnName("deal_id_exch");

                entity.Property(e => e.DealSource)
                    .HasColumnName("deal_source")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DealTime).HasColumnName("deal_time");

                entity.Property(e => e.DerCode)
                    .IsRequired()
                    .HasColumnName("der_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DirectionalQuantity).HasColumnName("directional_quantity");

                entity.Property(e => e.ExchFee)
                    .HasColumnName("exch_fee")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.IsVirtual)
                    .HasColumnName("is_virtual")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PortfolioId).HasColumnName("portfolio_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.TradeDate)
                    .HasColumnName("trade_date")
                    .HasColumnType("date");

                entity.Property(e => e.Volume)
                    .HasColumnName("volume")
                    .HasColumnType("decimal(28, 6)");
            });

            modelBuilder.Entity<FuturesQuotes>(entity =>
            {
                entity.HasKey(e => e.FutName)
                    .HasName("pk_futures_quotes");

                entity.ToTable("futures_quotes", "quik");

                entity.Property(e => e.FutName)
                    .HasColumnName("fut_name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DaysToExp).HasColumnName("days_to_exp");

                entity.Property(e => e.ExpDate)
                    .HasColumnName("exp_date")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FutCode)
                    .HasColumnName("fut_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastDeal)
                    .HasColumnName("last_deal")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.StepPrice)
                    .HasColumnName("step_price")
                    .HasColumnType("decimal(19, 9)");

                entity.Property(e => e.StepSize)
                    .HasColumnName("step_size")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Ticker)
                    .HasColumnName("ticker")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FuturesQuotesAccumulated>(entity =>
            {
                entity.ToTable("futures_quotes_accumulated", "quik");

                entity.HasIndex(e => new { e.LastDeal, e.FutCode, e.CreateDate })
                    .HasName("code_create_date");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.FutCode)
                    .HasColumnName("fut_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FutName)
                    .HasColumnName("fut_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastDeal)
                    .HasColumnName("last_deal")
                    .HasColumnType("decimal(19, 6)");
            });

            modelBuilder.Entity<FuturesSpec>(entity =>
            {
                entity.ToTable("futures_spec", "smile");

                entity.HasIndex(e => e.ShortName)
                    .HasName("ind_short_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BaseSec)
                    .HasColumnName("base_sec")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Board)
                    .HasColumnName("board")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Currency).HasColumnName("currency");

                entity.Property(e => e.FirstTradeDate)
                    .HasColumnName("first_trade_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.LastTradeDate)
                    .HasColumnName("last_trade_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Lot).HasColumnName("lot");

                entity.Property(e => e.Margin)
                    .HasColumnName("margin")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.SecId)
                    .IsRequired()
                    .HasColumnName("sec_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasColumnName("short_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StepPrice)
                    .HasColumnName("step_price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.StepSize)
                    .HasColumnName("step_size")
                    .HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<LimitsArch>(entity =>
            {
                entity.ToTable("limits_arch", "quik");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountCode)
                    .HasColumnName("account_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccumPnl)
                    .HasColumnName("accum_pnl")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentOpenPos)
                    .HasColumnName("current_open_pos")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.ExchFee)
                    .HasColumnName("exch_fee")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.FirmCode)
                    .HasColumnName("firm_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Limit)
                    .HasColumnName("limit")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.LimitType)
                    .HasColumnName("limit_type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OptionPremium)
                    .HasColumnName("option_premium")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.PrevLimit)
                    .HasColumnName("prev_limit")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.RealMargin)
                    .HasColumnName("real_margin")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.VarMargin)
                    .HasColumnName("var_margin")
                    .HasColumnType("decimal(19, 6)");
            });

            modelBuilder.Entity<MatlabHestonHedgingCalc>(entity =>
            {
                entity.ToTable("matlab_heston_hedging_calc", "quik");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BsDelta)
                    .HasColumnName("bs_delta")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.BsPortfolioPrice)
                    .HasColumnName("bs_portfolio_price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.BsVega)
                    .HasColumnName("bs_vega")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.FutPrice)
                    .HasColumnName("fut_price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.HestonDelta)
                    .HasColumnName("heston_delta")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.HestonPortfolioPrice)
                    .HasColumnName("heston_portfolio_price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.HestonVega)
                    .HasColumnName("heston_vega")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Sigma)
                    .HasColumnName("sigma")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.TimeToExp)
                    .HasColumnName("time_to_exp")
                    .HasColumnType("decimal(19, 6)");
            });

            modelBuilder.Entity<OptionsQuotes>(entity =>
            {
                entity.HasKey(e => e.OptName)
                    .HasName("PK__options___E3CACE2C367B793F");

                entity.ToTable("options_quotes", "quik");

                entity.Property(e => e.OptName)
                    .HasColumnName("opt_name")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Bid)
                    .HasColumnName("bid")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.ClearingQuote)
                    .HasColumnName("clearing_quote")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.DaysToExp).HasColumnName("days_to_exp");

                entity.Property(e => e.ExpDate)
                    .HasColumnName("exp_date")
                    .HasColumnType("date")
                    .HasComputedColumnSql("(CONVERT([date],[exp_date_quik_raw]))");

                entity.Property(e => e.ExpDateQuikRaw)
                    .HasColumnName("exp_date_quik_raw")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GoLong)
                    .HasColumnName("GO_long")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.GoShort)
                    .HasColumnName("GO_short")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Offer)
                    .HasColumnName("offer")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.OptCode)
                    .HasColumnName("opt_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OptType)
                    .HasColumnName("opt_type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Strike)
                    .HasColumnName("strike")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.TheorPrice)
                    .HasColumnName("theor_price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.UnderlyingCode)
                    .HasColumnName("underlying_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VolatilityExch)
                    .HasColumnName("volatility_exch")
                    .HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<OptionsQuotesArch>(entity =>
            {
                entity.ToTable("options_quotes_arch", "quik");

                entity.HasIndex(e => new { e.ExpDate, e.ClearingQuote, e.OptCode, e.CreateDate })
                    .HasName("NonClusteredIndex-20190222-185301");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bid)
                    .HasColumnName("bid")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.ClearingQuote)
                    .HasColumnName("clearing_quote")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DaysToExp).HasColumnName("days_to_exp");

                entity.Property(e => e.ExpDate)
                    .HasColumnName("exp_date")
                    .HasColumnType("date");

                entity.Property(e => e.GoLong)
                    .HasColumnName("GO_long")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.GoShort)
                    .HasColumnName("GO_short")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Offer)
                    .HasColumnName("offer")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.OptCode)
                    .IsRequired()
                    .HasColumnName("opt_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OptType)
                    .HasColumnName("opt_type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Strike)
                    .HasColumnName("strike")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.TheorPrice)
                    .HasColumnName("theor_price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.UnderlyingCode)
                    .HasColumnName("underlying_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VolatilityExch)
                    .HasColumnName("volatility_exch")
                    .HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<OptionsSpec>(entity =>
            {
                entity.ToTable("options_spec", "smile");

                entity.HasIndex(e => e.SecId)
                    .HasName("ind_sec_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Board)
                    .HasColumnName("board")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Currency).HasColumnName("currency");

                entity.Property(e => e.ExpDate)
                    .HasColumnName("exp_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.FirstTradeDate)
                    .HasColumnName("first_trade_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.MarginBuy)
                    .HasColumnName("margin_buy")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.MarginSell)
                    .HasColumnName("margin_sell")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.MarginSynth)
                    .HasColumnName("margin_synth")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.OptType).HasColumnName("opt_type");

                entity.Property(e => e.SecId)
                    .IsRequired()
                    .HasColumnName("sec_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasColumnName("short_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StepPrice)
                    .HasColumnName("step_price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.StepSize)
                    .HasColumnName("step_size")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Strike)
                    .HasColumnName("strike")
                    .HasColumnType("decimal(19, 3)");

                entity.Property(e => e.UnderlyingId).HasColumnName("underlying_id");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders", "smile");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasColumnName("account_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddInfo)
                    .HasColumnName("add_info")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DerCode)
                    .IsRequired()
                    .HasColumnName("der_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DirectionalQuantity).HasColumnName("directional_quantity");

                entity.Property(e => e.IsVirtual)
                    .HasColumnName("is_virtual")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("date");

                entity.Property(e => e.OrderIdExch).HasColumnName("order_id_exch");

                entity.Property(e => e.OrderStatus)
                    .HasColumnName("order_status")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PortfolioId).HasColumnName("portfolio_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.SentTime).HasColumnName("sent_time");

                entity.Property(e => e.Volume)
                    .HasColumnName("volume")
                    .HasColumnType("decimal(28, 6)");
            });

            modelBuilder.Entity<Portfolio1>(entity =>
            {
                entity.ToTable("portfolio", "smile");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnName("account")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IsVirt).HasColumnName("is_virt");

                entity.Property(e => e.PortfolioName)
                    .IsRequired()
                    .HasColumnName("portfolio_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RootSecurity)
                    .IsRequired()
                    .HasColumnName("root_security")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Positions>(entity =>
            {
                entity.HasKey(e => new { e.DerCode, e.PortfolioId, e.IsVirtual })
                    .HasName("pk");

                entity.ToTable("positions", "smile");

                entity.Property(e => e.DerCode)
                    .HasColumnName("der_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PortfolioId).HasColumnName("portfolio_id");

                entity.Property(e => e.IsVirtual).HasColumnName("is_virtual");

                entity.Property(e => e.EffectiveStrike)
                    .HasColumnName("effective_strike")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.ExchFee)
                    .HasColumnName("exch_fee")
                    .HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LastUpdateDate).HasColumnName("last_update_date");

                entity.Property(e => e.Margin)
                    .HasColumnName("margin")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Quntity)
                    .HasColumnName("quntity")
                    .HasColumnType("decimal(19, 6)");
            });

            modelBuilder.Entity<PositionsArch>(entity =>
            {
                entity.ToTable("positions_arch", "quik");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountCode)
                    .HasColumnName("account_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DerCode)
                    .HasColumnName("der_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DerType)
                    .HasColumnName("der_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EffectivePrice)
                    .HasColumnName("effective_price")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.ExpDate)
                    .HasColumnName("exp_date")
                    .HasColumnType("date");

                entity.Property(e => e.FirmCode)
                    .HasColumnName("firm_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.RealMargin)
                    .HasColumnName("real_margin")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.VarMargin)
                    .HasColumnName("var_margin")
                    .HasColumnType("decimal(19, 6)");
            });

            modelBuilder.Entity<QuotesArchive>(entity =>
            {
                entity.ToTable("quotes_archive");

                entity.HasIndex(e => new { e.High, e.Low, e.Open, e.Close, e.Vol, e.Date, e.Ticker, e.Period })
                    .HasName("NonClusteredIndex-20190129-224400");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Close)
                    .HasColumnName("close")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.High)
                    .HasColumnName("high")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Low)
                    .HasColumnName("low")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Open)
                    .HasColumnName("open")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Period)
                    .HasColumnName("period")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ticker)
                    .HasColumnName("ticker")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Vol).HasColumnName("vol");
            });

            modelBuilder.Entity<QuotesCandlesUpdatedFromTicks>(entity =>
            {
                entity.ToTable("quotes_candles_updated_from_ticks", "quik");

                entity.HasIndex(e => new { e.High, e.Low, e.Open, e.Close, e.DerCode, e.Date })
                    .HasName("der_code_date");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Close)
                    .HasColumnName("close")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.DerCode)
                    .HasColumnName("der_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.High)
                    .HasColumnName("high")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Low)
                    .HasColumnName("low")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Open)
                    .HasColumnName("open")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Period)
                    .HasColumnName("period")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ticker)
                    .HasColumnName("ticker")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
