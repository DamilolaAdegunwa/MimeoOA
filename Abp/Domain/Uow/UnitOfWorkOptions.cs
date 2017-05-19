using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Unit of work options.
    /// </summary>
    public class UnitOfWorkOptions: IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Scope option.
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// Is this UOW transactional?
        /// Uses default value if not supplied.
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// Timeout of UOW As milliseconds.
        /// Uses default value if not supplied.
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// If this UOW is transactional, this option indicated the isolation level of the transaction.
        /// Uses default value if not supplied.
        /// </summary>
        //public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// This option should be set to <see cref="TransactionScopeAsyncFlowOption.Enabled"/>
        /// if unit of work is used in an async scope.
        /// </summary>
        //public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }

        /// <summary>
        /// Can be used to enable/disable some filters. 
        /// </summary>
        public List<DataFilterConfiguration> FilterOverrides { get; private set; }
        TransactionScopeOption IUnitOfWorkDefaultOptions.Scope { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IUnitOfWorkDefaultOptions.IsTransactional { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IReadOnlyList<DataFilterConfiguration> Filters => throw new NotImplementedException();

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkOptions"/> object.
        /// </summary>
        public UnitOfWorkOptions()
        {
            FilterOverrides = new List<DataFilterConfiguration>();
        }

        internal void FillDefaultsForNonProvidedOptions(IUnitOfWorkDefaultOptions defaultOptions)
        {
            //TODO: Do not change options object..?

            if (!IsTransactional.HasValue)
            {
                IsTransactional = defaultOptions.IsTransactional;
            }

            if (!Scope.HasValue)
            {
                Scope = defaultOptions.Scope;
            }

            if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
            {
                Timeout = defaultOptions.Timeout.Value;
            }
        }

        public void RegisterFilter(string filterName, bool isEnabledByDefault)
        {
            //throw new NotImplementedException();
        }

        public void OverrideFilter(string filterName, bool isEnabledByDefault)
        {
            //throw new NotImplementedException();
        }
    }
}
