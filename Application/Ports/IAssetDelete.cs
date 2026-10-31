using Core.Entities;

namespace Application.Ports;
public interface IAssetDelete
{
    void Delete(Guid assetId);
}