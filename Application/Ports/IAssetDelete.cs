using Core.Entities;

namespace Application.Ports;
public interface IAssetDelete
{
    bool Delete(Guid assetId);
}